using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Wander,
    Follow,
    Die,
    Attack,
    Freeze,
    Flee,
};

public enum EnemyType
{
    Melee,
    Ranged,
};


public class EnemyController : MonoBehaviour
{

    GameObject player;

    public EnemyState currentState = EnemyState.Idle;

    public EnemyType enemyType;

    public float health = 10;

    private float maxHealth = 10;

    public float range = 5f;

    public float speed = 3f;

    public float accelerationValue = 0.1f;

    public Vector2 acceleration;

    public Vector2 desiredVelocity;

    public float attackRange;

    public float coolDown;

    private bool coolDownAttack = false;

    private bool chooseDir = false;

    private bool dead = false;

    public bool notInRoom = false;

    public GameObject bulletPrefab;

    private Vector3 randomDir;

    private Rigidbody2D rb;

    public bool isFlying = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();
        currentState = EnemyState.Idle;
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case (EnemyState.Wander):
                Wander();
                break;
            case (EnemyState.Follow):
                Follow();
                break;
            case (EnemyState.Die):
                Death();
                break;
            case (EnemyState.Attack):
                Attack();
                break;
            case (EnemyState.Idle):
                Idle();
                break;
        }
        if (!notInRoom)
        {
            if (IsPlayerInRange(range) && currentState != EnemyState.Die)
            {
                currentState = EnemyState.Follow;
            }
            else if (!IsPlayerInRange(range) && currentState != EnemyState.Die)
            {
                currentState = EnemyState.Wander;
            }
            if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
            {
                currentState = EnemyState.Attack;
            }
        }
        else
        {
            currentState = EnemyState.Idle;
        }
    }

    private bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    private IEnumerator ChooseDirection()
    {
        chooseDir = true;
        yield return new WaitForSeconds(Random.Range(1f, 3f)); //wählt in zufälligen Abständen neue Richtung //wählt zufällige Richtung
        rb.rotation = Random.Range(0f, 360f);
        chooseDir = false;
    }

    //bewegt sich ziellos herum
    void Wander()
    {
        if (!chooseDir)
        {
            StartCoroutine(ChooseDirection());
        }
        MoveForward();
        if (IsPlayerInRange(range))
        {
            currentState = EnemyState.Follow;
        }
    }

    void Idle()
    {
        rb.velocity = new Vector2(0, 0);
    }

    void MoveForward()
    {
        float angle = rb.rotation * Mathf.Deg2Rad;
        acceleration = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * accelerationValue;
        desiredVelocity = acceleration.normalized * speed;
        if (rb.velocity.magnitude == 0)
        {
            rb.velocity = acceleration;
        }
        else
        {
            if (rb.velocity != desiredVelocity)
            {
                rb.velocity += acceleration;
            }
            if (rb.velocity.magnitude > speed)
            {

                rb.velocity = speed * rb.velocity.normalized;
            }
        }
    }

    void SlowDown()
    {
        float angle = rb.rotation * Mathf.Deg2Rad;
        acceleration = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * accelerationValue;
        desiredVelocity = new Vector2(0, 0);
        if (rb.velocity != desiredVelocity)
        {
            Vector2 vchange = accelerationValue * rb.velocity;
            if (vchange.magnitude <= rb.velocity.magnitude)
            {
                rb.velocity -= accelerationValue * rb.velocity;
            }
            else
            {
                rb.velocity = desiredVelocity;
            }
        }
    }

    //Bewegt sich auf den spieler zu
    void Follow()
    {
        rb.rotation = Vector2.SignedAngle(new Vector2(1, 0), player.transform.position - transform.position);
        MoveForward();
    }

    //Fügt dem Spieler Schaden zu
    void Attack()
    {
        if (!coolDownAttack)
        {
            switch (enemyType)
            {
                case (EnemyType.Melee):
                    GameController.DamagePlayer(1);
                    StartCoroutine(CoolDown());
                    break;
                case (EnemyType.Ranged):
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
                    bullet.GetComponent<BulletController>().GetPlayer(player.transform);
                    bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
                    bullet.GetComponent<BulletController>().isEnemyBullet = true;
                    StartCoroutine(CoolDown());
                    break;
            }
        }
        else
        {
            SlowDown();
        }
    }

    //Verarbeitet den Angriffscooldown
    private IEnumerator CoolDown()
    {
        coolDownAttack = true;
        yield return new WaitForSeconds(coolDown);
        coolDownAttack = false;
    }

    public void Death()
    {
        RoomController.instance.StartCoroutine(RoomController.instance.RoomCoroutine());
        Destroy(gameObject);
    }

    public void DamageEnemy(float damage)
    {
        health = Mathf.Max(0, health - damage);
        if (health <= 0)
        {
            Death();
        }
    }
}
