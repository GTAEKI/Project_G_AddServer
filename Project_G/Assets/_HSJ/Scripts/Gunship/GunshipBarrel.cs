using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using static Define;

public class GunshipBarrel : MonoBehaviour
{

    private Gunship gunship;
    private Camera mainCamera;
    private GameInput gameInput;
  
    private Vector3 bottomLeftScreen;
    private Vector3 bottomLeftWorld;
    private Vector3 barrelOffset = new Vector3(1f,0f,0f);
    private float bulletSpeed = 100f;
    private const float bulletDelay = 0.1f;

    public EColorType BulletType { get; private set; }

    [SerializeField]
    private LayerMask layer;

    int count = 0;

    void Awake()
    {
        Init();
    }

    void Init()
    {
        gunship = GetComponentInParent<Gunship>();

        gameInput = gunship.GameInput;
        mainCamera = gunship.MainCamera;

        BulletType = EColorType.White;
        InitInputEvent();
    }
    // 이벤트 등록
    void InitInputEvent()
    {
        gameInput.OnBulletChange_Left += ChangeBullet_Left;
        gameInput.OnBulletChange_Right += ChangeBullet_Right;
    }

    private float fireSoundCooldown = 0.1f;
    private float lastFireSoundTime = 0f;

    void Update()
    {
        transform.position = GetScreenToWorldPosition();

        if (!Managers.Game.IsGameEnded && gameInput.GetIsAttack())
        {
            GameInput_Fire();

            if (Time.time - lastFireSoundTime >= fireSoundCooldown)
            {
                Managers.Sound.Play(ESound.Effect, "GunShoot");
                lastFireSoundTime = Time.time;
            }
        }
    }


    void GameInput_Fire()
    {
        RaycastHit hit;
       
        Vector3 targetPoint;
        if(Physics.Raycast(mainCamera.transform.position,mainCamera.transform.forward, out hit, float.MaxValue, layer))
        {
            targetPoint = hit.point;
        }
        else
        {
            return;
        }
        Vector3 dir = (targetPoint - transform.position).normalized;

        GameObject bullet =
            Managers.Projectile.Dequeue(
                transform.position,
                mainCamera.transform.rotation,
                "Bullet");

        GunshipBullet gBullet = bullet.GetComponent<GunshipBullet>();

        gBullet.InitBulletColor(BulletType);
        gBullet.ShotBullet(dir, bulletSpeed);


    }


    void ChangeBullet_Left(object sender, System.EventArgs e)
    {
        if(BulletType <= EColorType.White)
        {
            BulletType = EColorType.Yellow;
            return;
        }
        BulletType -= 1;
    }

    void ChangeBullet_Right(object sender, System.EventArgs e)
    {
        if(BulletType >= EColorType.Yellow)
        {
            BulletType = EColorType.White;
            return;
        }
        BulletType += 1;
    }


    // 좌하단 스크린 포지션을 월드 포지션으로 변경
    Vector3 GetScreenToWorldPosition()
    {
        bottomLeftScreen = new Vector3(0, 0, mainCamera.nearClipPlane);
        bottomLeftWorld = mainCamera.ScreenToWorldPoint(bottomLeftScreen );
        bottomLeftWorld -= barrelOffset;
        
        return bottomLeftWorld;
    }
}
