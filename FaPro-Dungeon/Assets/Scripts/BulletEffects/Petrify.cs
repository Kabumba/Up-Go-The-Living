using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Petrify : BulletEffect
{
    private bool petrifying;

    public override void OnEnemyHit(Collider2D collision)
    {
        if (petrifying)
        {
            collision.GetComponent<EnemyController>().ApplyStatusEffect<Petrified>();
        }
    }

    public override void OnInstantiate()
    {
        float rand = Random.Range(0, 100);
        if (rand >= 85)
        {
            petrifying = true;
            Color temp = gameObject.GetComponent<SpriteRenderer>().color;
            temp.b = Mathf.Max(255, temp.b + 100f);
            gameObject.GetComponent<SpriteRenderer>().color = temp;
        }
    }
}
