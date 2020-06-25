using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rage : BulletEffect
{
    public override void OnEnemyHit(Collider2D collision)
    {
        GameController.ChangeDamage(0.5f);
        GameController.DamageThroughRage += 0.5f;
    }
}
