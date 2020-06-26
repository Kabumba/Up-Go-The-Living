using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampupDamage : BulletEffect
{
    private void Start()
    {
        defaultEnemyHit = false;
    }
    public override void OnEnemyHit(Collider2D collision)
    {
        float enhancedDamage = GameController.GetEffectiveDamage() + gameObject.GetComponent<BulletController>().distanceTravelled * 0.5f;
        collision.GetComponent<EnemyController>().DamageEnemy(enhancedDamage);
        Destroy(gameObject);
    }
}
