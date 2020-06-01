using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float range;

    public float damage;

    public float lifeTime;

    public float knockback = 5f;

    public bool isEnemyBullet = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!isEnemyBullet)
        {
            damage = GameController.Damage;
            range = GameController.Range;
            lifeTime = range / 30f;
            transform.localScale = new Vector2(GameController.BulletSize, GameController.BulletSize);
            StartCoroutine(DeathDelay());
        }
        
    }

    //Sorgt dafür, dass das Projektil zerstört wird, wenn es sich zu weit von seiner startpostion entfernt hat
    public IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger)
        {
            return;
        }

        if ("Enemy".Equals(collision.tag))
        {
            if (isEnemyBullet)
            {
            }
            else
            {
                collision.gameObject.GetComponent<EnemyController>().DamageEnemy(damage);
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(gameObject.GetComponent<Rigidbody2D>().velocity * knockback, ForceMode2D.Impulse);

                Destroy(gameObject);
            }
        }
        if ("Player".Equals(collision.tag))
        {
            if (!isEnemyBullet)
            {
            }
            else
            {
                GameController.DamagePlayer(1);
                Destroy(gameObject);
            }
        }
        if ("Projectile".Equals(collision.tag))
        {
            if (isEnemyBullet == collision.GetComponent<BulletController>().isEnemyBullet)
            {
            }
            else
            {
            }
        }
    }
}
