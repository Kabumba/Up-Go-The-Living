using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingShots : Item
{
    public Sprite pierceSprite;
    public override void OnPickup()
    {
        GameObject bulletPrefab = GameController.changeableBullet;
        bulletPrefab.GetComponent<SpriteRenderer>().sprite = pierceSprite;
        bulletPrefab.GetComponent<SpriteRenderer>().sortingLayerName = "Pierce";
        bulletPrefab.GetComponent<BulletController>().isPiercingShot = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<ShooterController>().bulletPrefab = bulletPrefab;
        GameObject.Find("Main Left").GetComponent<BulletShooter>().bulletPrefab = bulletPrefab;
        GameObject.Find("Main Right").GetComponent<BulletShooter>().bulletPrefab = bulletPrefab;
    }
}
