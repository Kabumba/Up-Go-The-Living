using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BulletEffect : MonoBehaviour
{
    //Soll das Projektil nach den Bulleteffects das normale Verhalten aufweisen, z.B. Schaden verursachen und zerstört werden?
    public bool defaultPlayerHit = true;

    public bool defaultEnemyHit = true;

    public bool defaultProjectileHit = true;

    public bool defaultObstacleHit = true;

    public bool isSplatter = false;

    public bool successfulPoison = false;

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

}

public class BulletController : MonoBehaviour
{
    
    public float damage;

    public float lifeTime;

    public float knockback = 5f;

    public bool isEnemyBullet;

    public Rigidbody2D rb;

    public MovementController mvc;

    public float distanceTravelled = 0;

    Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        if (!isEnemyBullet)
        {
            damage = GameController.GetEffectiveDamage();
            lifeTime = GameController.GetBulletLifeTime();
        }
        mvc = gameObject.GetComponent<MovementController>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        lastPosition = transform.position;
        StartCoroutine(DeathDelay());

    }

    private void Awake()
    {
        if (!isEnemyBullet)
        {
            transform.localScale = new Vector2(GameController.GetBulletSize(), GameController.GetBulletSize());
        }
        mvc.desiredVelocity = rb.velocity;
    }

    //Sorgt dafür, dass das Projektil zerstört wird, wenn es sich zu weit von seiner startpostion entfernt hat
    public IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void Update()
    {
        foreach (BulletEffect be in GetComponents<BulletEffect>())
        {
            be.Tick();
        }
        distanceTravelled += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
                        Destroy(gameObject);
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
                        Destroy(gameObject);
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
                if (collision.GetComponent<Collider2D>().isTrigger)
                {
                    return;
                }
                foreach (BulletEffect be in GetComponents<BulletEffect>())
                {
                    be.OnObstacleHit(collision);
                    defaultBehavior = defaultBehavior && be.defaultObstacleHit;
                }
                if (defaultBehavior)
                {
                    Destroy(gameObject);
                }

                break;
        }
    }
}
