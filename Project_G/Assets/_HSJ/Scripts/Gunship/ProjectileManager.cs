using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager
{
    private Queue<GameObject> bullets;
    private Queue<GameObject> particles;
    private const string RESOURCE_BULLET = "Bullet";
    private const string RESOURCE_HIT = "Bullet_Hit";
    public ProjectileManager()
    {
        Init();
    }
    private void Init()
    {
        bullets = new Queue<GameObject>();
        particles = new Queue<GameObject>();        
    }

    private GameObject CreateBullet()
    {
        GameObject bulletObject = Managers.Resource.Instantiate(RESOURCE_BULLET);
        bulletObject.SetActive(false);
        bullets.Enqueue(bulletObject);
        return bulletObject;
    }

    public GameObject CreateHitFx()
    {
        GameObject hitFxObject = Managers.Resource.Instantiate(RESOURCE_HIT);
        hitFxObject.SetActive(false);
        particles.Enqueue(hitFxObject);
        return hitFxObject;
    }

    public void Enqueue(GameObject go, string type)
    {
        go.SetActive(false);
        go.transform.position = Vector3.zero;
        go.transform.rotation = Quaternion.identity;
        switch (type)
        {
            case "Bullet":
                bullets.Enqueue(go);

                break;
            case "Fx":
                particles.Enqueue(go);
                break;
        }
    }

    public GameObject Dequeue(Vector3 pos, Quaternion rot , string type)
    {
        GameObject go = default;

        switch (type)
        {
            case "Bullet":
                if (bullets.Count == 0)
                {
                    go = CreateBullet();
                }
                else
                {
                    go = bullets.Dequeue();
                }
                break;
        }
        go.transform.position = pos;
        go.transform.rotation = rot;
        go.SetActive(true);
        return go;
    }

    public GameObject Dequeue(Vector3 pos, Vector3 dir, string type)
    {
        GameObject go = default;

        switch (type)
        {
            case "Fx":
                if (particles.Count == 0)
                {
                    go = CreateHitFx();
                }
                else
                {
                    go = particles.Dequeue();
                }
                break;
        }
        go.transform.position = pos;
        go.transform.forward = dir;
        go.SetActive(true);
        return go;
    }


    public void Clear()
    {
        bullets.Clear();
        particles.Clear();
    }

    
    
}
