using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : BulletEffect
{
    public bool poisonous;

    public float chance = 1;

    public override void OnEnemyHit(Collider2D collision)
    {
        if (poisonous)
        {
            collision.GetComponent<EnemyController>().ApplyStatusEffect<Poisoned>();
        }
    }

    public override void OnInstantiate()
    {
        float rand = Random.value;
        if (rand < chance)
        {
            poisonous = true;
            //gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            Color temp = gameObject.GetComponent<SpriteRenderer>().color;
            temp.g = Mathf.Max(255, temp.g + 100f);
            gameObject.GetComponent<SpriteRenderer>().color = temp;
        }
    }
}
