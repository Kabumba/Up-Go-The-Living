using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Petrify : BulletEffect
{
    public bool petrifying;

    public float chance = 0.15f;

    public override void OnEnemyHit(Collider2D collision)
    {
        if (petrifying)
        {
            collision.GetComponent<EnemyController>().ApplyStatusEffect<Petrified>();
        }
    }

    public override void OnInstantiate()
    {
        float rand = Random.value;
        if (rand < chance)
        {
            petrifying = true;
            Color temp = gameObject.GetComponent<SpriteRenderer>().color;
            temp.b = Mathf.Max(255, temp.b + 100f);
            gameObject.GetComponent<SpriteRenderer>().color = temp;
        }
    }
}
