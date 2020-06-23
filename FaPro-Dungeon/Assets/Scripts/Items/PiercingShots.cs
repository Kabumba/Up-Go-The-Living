using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingShots : Item
{
    public Sprite pierceSprite;
    public override void OnPickup()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        ShooterController sc = player.GetComponent<ShooterController>();
        GameObject bulletPrefab = GameController.changeableBullet;
        bulletPrefab.GetComponent<SpriteRenderer>().sprite = pierceSprite;
        bulletPrefab.GetComponent<SpriteRenderer>().sortingLayerName = "Pierce";
        bulletPrefab.GetComponent<BulletController>().isPiercingShot = true;
        sc.bulletPrefab = bulletPrefab;
        foreach(BulletShooter bs in sc.bulletShooters)
        {
            bs.bulletPrefab = bulletPrefab;
        }
    }
}
