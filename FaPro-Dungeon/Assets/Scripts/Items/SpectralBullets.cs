using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.WSA;

public class SpectralBullets : Item
{
    public override void OnPickup()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        ShooterController sc = player.GetComponent<ShooterController>();
        GameObject bulletPrefab = GameController.changeableBullet;
        bulletPrefab.layer = 10;
        sc.bulletPrefab = bulletPrefab;
        foreach(BulletShooter bs in sc.bulletShooters)
        {
            bs.bulletPrefab = bulletPrefab;
        }
    }
}
