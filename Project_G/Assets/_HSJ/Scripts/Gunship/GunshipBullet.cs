using UnityEngine;
using System.Collections;
using static Define;

public class GunshipBullet : MonoBehaviour
{
    #region SerializeField

    [SerializeField]
    private ParticleSystem bulletPS;
    [SerializeField]
    private Light bulletLight;
    [SerializeField]
    private Rigidbody rigid;
    #endregion

    #region Colors 
    [SerializeField]
    [ColorUsage(false,false)]
    private Color White;
    [SerializeField]
    [ColorUsage(false, false)]
    private Color Red;
    [SerializeField]
    [ColorUsage(false, false)]
    private Color Yellow;
    #endregion

    public float Damage { get; private set; } = 10f;

    private ParticleSystem.MainModule main;
    
    private Vector3 hitPSdir;
    EColorType colortype = EColorType.White;

    void Awake()
    {
        // Get ParticleSystem Mainmodule
        main = bulletPS.main;
    }


    void Update()
    {
        if(gameObject.transform.position.y < -1000)
        {
            Managers.Projectile.Enqueue(this.gameObject, "Bullet");
        }
        
    }


    public void InitBulletColor(EColorType type = EColorType.White)
    {
        switch (type)
        {
            case EColorType.Red:
                colortype = EColorType.Red;
                main.startColor = Red;
                bulletLight.color = Red;
                break;
            case EColorType.Yellow:
                colortype = EColorType.Yellow;
                main.startColor = Yellow;
                bulletLight.color = Yellow;
                break;
            default:
                colortype = EColorType.White;
                main.startColor = White;
                bulletLight.color = White;
                break;
        }        
    }

    public void ShotBullet(Vector3 dir, float bulletSpeed)
    {
        rigid.constraints = RigidbodyConstraints.None;
        hitPSdir = dir;
        rigid.AddForce(dir * bulletSpeed, ForceMode.VelocityChange);
        
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.CalDamage(Damage, colortype);
            Managers.Projectile.Enqueue(this.gameObject, "Bullet");
            GameObject fx = Managers.Projectile.Dequeue(other.ClosestPoint(transform.position) - hitPSdir, -hitPSdir, "Fx");
            fx.GetComponent<ParticleColorChange>().ChangeParticleColor(colortype);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            GameObject fx = Managers.Projectile.Dequeue(other.ClosestPoint(transform.position) - hitPSdir, -hitPSdir , "Fx");
            fx.GetComponent<ParticleColorChange>().ChangeParticleColor(colortype);

            Managers.Projectile.Enqueue(this.gameObject, "Bullet");
        }
        rigid.constraints = RigidbodyConstraints.FreezeAll;
    }
}
