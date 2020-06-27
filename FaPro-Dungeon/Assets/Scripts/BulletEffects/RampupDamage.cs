using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampupDamage : BulletEffect
{
    public override void OnEnemyHit(Collider2D collision)
    {
        gameObject.GetComponent<BulletController>().damage += gameObject.GetComponent<BulletController>().distanceTravelled * 0.5f;
    }
}
