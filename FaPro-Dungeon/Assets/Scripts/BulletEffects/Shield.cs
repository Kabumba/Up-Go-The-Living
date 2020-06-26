using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : BulletEffect
{
    private void Start()
    {
        defaultProjectileHit = false;
    }
    public override void OnProjectileHit(Collider2D collision)
    {
        if (collision.GetComponent<BulletController>().isEnemyBullet)
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
