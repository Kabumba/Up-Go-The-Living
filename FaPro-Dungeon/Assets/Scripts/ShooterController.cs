using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    public GameObject bulletPrefab;

    public float bulletSpeed;

    public float fireDelay;

    public float velocityAddedToBullet;

    public Rigidbody2D rb;

    public List<BulletShooter> bulletShooters;

    private void Start()
    {
        InitializeBulletshooters();
    }

    private void Awake()
    {
        foreach (BulletShooter bs in gameObject.GetComponentsInChildren<BulletShooter>())
        {
            Add(bs);
        }
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        foreach (BulletShooter bs in bulletShooters)
        {
            bs.fireTimeDelay = fireDelay;
        }
    }

    public void TryShoot()
    {
        foreach (BulletShooter bs in bulletShooters)
        {
            bs.Shoot();
        }
    }

    public void Add(BulletShooter bs)
    {
        bulletShooters.Add(bs);
    }

    public void AddBulletEffectToAll(BulletEffect be)
    {
        foreach (BulletShooter bs in bulletShooters)
        {
            bs.bulletEffects.Add(be);
        }
    }

    public void InitializeBulletshooters()
    {
        foreach (BulletShooter bs in bulletShooters)
        {
            bs.shooterController = this;
            bs.fireTimeDelay = fireDelay;
            bs.bulletPrefab = bulletPrefab;
        }
    }

    public void UpdateBulletEffects()
    {
        foreach (BulletShooter bs in bulletShooters)
        {
            bs.bulletEffects = new List<BulletEffect>(bulletShooters[0].bulletEffects);
        }
    }

    public void resetLastFire()
    {
        foreach (BulletShooter bs in bulletShooters)
        {
            bs.resetLastFire();
        }
    }
}
