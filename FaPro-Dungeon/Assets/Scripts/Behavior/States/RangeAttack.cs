using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : State
{
    public float bulletSpeed = 7f;
    public float range = 30f;

    public RangeAttack(EnemyController character) : base(character)
    {
        name = "RangeAttack";
    }

    public override void OnUpdate()
    {
        character.rb.rotation = Vector2.SignedAngle(new Vector2(1, 0), character.player.transform.position - character.transform.position);
        if (!character.coolDownAttack)
        {
            GameObject bullet = EnemyController.Instantiate(character.bulletPrefab, character.transform.position, character.transform.rotation) as GameObject;
            BulletController bc = bullet.GetComponent<BulletController>();
            Rigidbody2D rb = bullet.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            bc.isEnemyBullet = true;
            bc.lifeTime = range / 30f;
            rb.velocity = new Vector3(
                        bullet.transform.right.x * bulletSpeed,
                        bullet.transform.right.y * bulletSpeed,
                        0
                        );
            bc.StartCoroutine(bc.DeathDelay());
            character.StartCoroutine(character.CoolDown());
        }
        character.SlowDown();
    }
}
