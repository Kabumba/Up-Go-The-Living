using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rage : BulletEffect
{
    public override void OnEnemyHit(Collider2D collision)
    {
        GameController.AddDamage(0.5f);
        GameController.damageThroughRage += 0.5f;
    }
}
