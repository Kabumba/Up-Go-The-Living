using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.WSA;

public class SpectralBullets : Item
{
    public override void OnPickup()
    {
        GameObject bulletPrefab = GameController.changeableBullet;
        bulletPrefab.layer = 10;
        GameObject.Find("Player").GetComponent<ShooterController>().bulletPrefab = bulletPrefab;
        GameObject.Find("Main Left").GetComponent<BulletShooter>().bulletPrefab = bulletPrefab;
        GameObject.Find("Main Right").GetComponent<BulletShooter>().bulletPrefab = bulletPrefab;
    }
}
