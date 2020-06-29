using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fear : BulletEffect
{
    public bool scary;

    public float chance = 0.15f;

    public override void OnEnemyHit(Collider2D collision)
    {
        if (scary)
        {
            collision.GetComponent<EnemyController>().ApplyStatusEffect<Feared>();
        }
    }

    public override void OnInstantiate()
    {
        float rand = Random.value;
        if (rand < chance)
        {
            scary = true;
            Color temp = gameObject.GetComponent<SpriteRenderer>().color;
            temp.g = Mathf.Max(255, temp.g + 50f);
            temp.b = Mathf.Max(255, temp.b + 50f);
            temp.r = Mathf.Max(255, temp.r + 50f);
            gameObject.GetComponent<SpriteRenderer>().color = temp;
        }
    }
}
