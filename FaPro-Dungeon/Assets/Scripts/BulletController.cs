using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BulletEffect : MonoBehaviour
{
    public BulletController bulletController;

    //Soll das Projektil nach den Bulleteffects das normale Verhalten aufweisen, z.B. Schaden verursachen und zerstört werden?
    public bool defaultPlayerHit = true;

    public bool defaultEnemyHit = true;

    public bool defaultProjectileHit = true;

    public bool defaultObstacleHit = true;

    //Wird aufgerufen wenn das Projektil einen nicht-freundlichen Spieler trifft.
    public virtual void OnPlayerHit(Collider2D collision)
    {

    }

    //Wird aufgerufen wenn das Projektil einen nicht-freundlichen Gegner trifft.
    public virtual void OnEnemyHit(Collider2D collision)
    {

    }

    //Wird aufgerufen wenn das Projektil ein anderes nicht-freundliches Projektil trifft
    public virtual void OnProjectileHit(Collider2D collision)
    {

    }

    //Wird aufgerufen wenn das Projektil ein Hindernis trifft.
    public virtual void OnObstacleHit(Collider2D collision)
    {

    }

    //Wird aufgerufen wenn das Projektil zerstört wird.
    public virtual void OnDestroy()
    {

    }

    public virtual void OnInstantiate()
    {

    }

    //Wird in jedem Frame einmal aufgerufen
    public virtual void Tick()
    {

    }

    private void Start()
    {
        bulletController = gameObject.GetComponent<BulletController>();
    }
}

public class BulletController : MonoBehaviour
{

    public float range;

    public float damage;

    public float lifeTime;

    public float knockback = 5f;

    public bool isEnemyBullet;

    public bool isPiercingShot;

    public Rigidbody2D rb;

    public MovementController mvc;

    // Start is called before the first frame update
    void Start()
    {
        if (!isEnemyBullet)
        {
            damage = GameController.Damage;
            range = GameController.Range;
            lifeTime = range / 30f;
        }
        mvc = gameObject.GetComponent<MovementController>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(DeathDelay());

    }

    private void Awake()
    {
        if (!isEnemyBullet)
        {
            transform.localScale = new Vector2(GameController.BulletSize, GameController.BulletSize);
        }
        mvc.desiredVelocity = rb.velocity;
    }

    //Sorgt dafür, dass das Projektil zerstört wird, wenn es sich zu weit von seiner startpostion entfernt hat
    public IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy();
    }

    private void Update()
    {
        foreach (BulletEffect be in GetComponents<BulletEffect>())
        {
            be.Tick();
        }
    }

    public void Destroy()
    {
        foreach (BulletEffect be in GetComponents<BulletEffect>())
        {
            be.OnDestroy();
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().isTrigger)
        {
            return;
        }
        bool defaultBehavior = true;
        switch (collision.tag)
        {
            case ("Enemy"):
                if (!isEnemyBullet)
                {
                    foreach (BulletEffect be in GetComponents<BulletEffect>())
                    {
                        be.OnEnemyHit(collision);
                        defaultBehavior = defaultBehavior && be.defaultEnemyHit;
                    }
                    if (defaultBehavior)
                    {
                        collision.GetComponent<EnemyController>().DamageEnemy(damage);
                        collision.GetComponent<Rigidbody2D>().AddForce(gameObject.GetComponent<Rigidbody2D>().velocity * knockback, ForceMode2D.Impulse);
                        if (!isPiercingShot)
                        {
                            Destroy();
                        }
                    }
                }
                break;
            case ("Player"):
                if (isEnemyBullet)
                {
                    foreach (BulletEffect be in GetComponents<BulletEffect>())
                    {
                        be.OnPlayerHit(collision);
                        defaultBehavior = defaultBehavior && be.defaultPlayerHit;
                    }
                    if (defaultBehavior)
                    {
                        GameController.DamagePlayer(1);
                        Destroy();
                    }
                }
                break;
            case ("Projectile"):
                if (isEnemyBullet != collision.GetComponent<BulletController>().isEnemyBullet)
                {
                    foreach (BulletEffect be in GetComponents<BulletEffect>())
                    {
                        be.OnProjectileHit(collision);
                        defaultBehavior = defaultBehavior && be.defaultProjectileHit;
                    }
                    if (defaultBehavior)
                    {
                    }
                }
                break;
            default:
                foreach (BulletEffect be in GetComponents<BulletEffect>())
                {
                    be.OnObstacleHit(collision);
                    defaultBehavior = defaultBehavior && be.defaultObstacleHit;
                }
                if (defaultBehavior)
                {
                    Destroy();
                }

                break;
        }
    }
}
