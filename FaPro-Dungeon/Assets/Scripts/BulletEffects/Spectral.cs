using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spectral : BulletEffect
{
    // Start is called before the first frame update
    void Start()
    {
        defaultEnemyHit = false;
        defaultObstacleHit = false;
    }

    public override void OnEnemyHit(Collider2D collision)
    {
        collision.GetComponent<EnemyController>().DamageEnemy(bulletController.damage);
    }
}
