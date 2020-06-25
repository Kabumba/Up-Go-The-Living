using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polyphemus : BulletEffect
{
    public float bulletSize = 1f;

    private void Start()
    {
        defaultEnemyHit = false;
    }

    public override void Tick()
    {
        float scale = GetComponent<BulletController>().damage / GameController.GetEffectiveDamage();
        transform.localScale = new Vector2(bulletSize*scale, bulletSize*scale);
    }

    public override void OnEnemyHit(Collider2D collision)
    {
        float enemyHealth = collision.gameObject.GetComponent<EnemyController>().health;
        if (GetComponent<BulletController>().damage >= enemyHealth)
        {
            defaultEnemyHit = false;
            collision.GetComponent<EnemyController>().DamageEnemy(GetComponent<BulletController>().damage);
            GetComponent<BulletController>().damage -= enemyHealth;
        }
        else
        {
            defaultEnemyHit = true;
        }
    }
}
