using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polyphemus : BulletEffect
{
    private void Start()
    {
        defaultDestroyOnEnemyHit = false;
    }

    public override void Tick()
    {
        float scale = GetComponent<BulletController>().damage / GameController.GetEffectiveDamage();
        transform.localScale = new Vector2(GameController.GetBulletSize()*scale, GameController.GetBulletSize() * scale);
    }

    public override void OnEnemyHit(Collider2D collision)
    {
        float enemyHealth = collision.gameObject.GetComponent<EnemyController>().health;
        if (GetComponent<BulletController>().damage >= enemyHealth)
        {
            defaultDamageEnemy = false;
            collision.GetComponent<EnemyController>().DamageEnemy(GetComponent<BulletController>().damage);
            GetComponent<BulletController>().damage -= enemyHealth;
        }
        else
        {
            defaultDestroyOnEnemyHit = true;
        }
    }
}
