using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : BulletEffect
{
    private int rand;

    private void Start()
    {
        defaultEnemyHit = false;
    }
    public override void OnEnemyHit(Collider2D collision)
    {
        collision.GetComponent<EnemyController>().DamageEnemy(GameController.GetEffectiveDamage());
        if (rand >= 90)
        {
            collision.GetComponent<EnemyController>().PoisonDamage();
        }
        Destroy(gameObject);
    }

    public override void OnInstantiate()
    {
        rand = Random.Range(0, 100);
        if (rand >= 90 || successfulPoison)
        {
            successfulPoison = true;
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
}
