using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pierce : BulletEffect
{
    private void Start()
    {
        defaultEnemyHit = false;
    }

    public override void OnEnemyHit(Collider2D collision)
    {
        collision.GetComponent<EnemyController>().DamageEnemy(GameController.Damage);
    }

    public override void OnInstantiate()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("PierceProjectile");
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    }
}
