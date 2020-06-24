using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polyphemus : BulletEffect
{
    private void Start()
    {
        defaultEnemyHit = false;
    }

    public override void Tick()
    {
        float scale = GetComponent<BulletController>().damage / GameController.GetDamage();
        transform.localScale = new Vector2(GameController.BulletSize*scale, GameController.BulletSize*scale);
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
