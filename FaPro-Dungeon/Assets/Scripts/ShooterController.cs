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

    public float damage = GameController.Damage * GameController.DamageMultiplier;

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
        damage = GameController.Damage * GameController.DamageMultiplier;
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

    public void InitializeBulletshooters()
    {
        foreach (BulletShooter bs in bulletShooters)
        {
            bs.shooterController = this;
            bs.fireTimeDelay = fireDelay;
            bs.bulletPrefab = bulletPrefab;
        }
    }
}
