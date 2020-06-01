using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Wander,
    Follow,
    Attack,
    Freeze,
    Flee,
};

public enum EnemyType
{
    Melee,
    Ranged,
};


public abstract class State
{
    protected EnemyController character;

    public string name;

    public abstract void OnUpdate();

    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

    public State(EnemyController character)
    {
        this.character = character;
    }
}

public abstract class AI : MonoBehaviour
{
    protected EnemyController character;

    public State currentState;

    //Legt fest wann unter welchen Umständen in welchen Zustand gewechselt werden soll
    public abstract void StateChanges();

    public void Awake()
    {
        this.character = gameObject.GetComponent<EnemyController>();
        currentState = new Idle(character);
    }

    //Wechselt in einen neuen Zustand
    public void SetState(State state)
    {
        if (currentState != null)
        {
            if (currentState.name.Equals(state.name))
            {
                return;
            }
            currentState.OnStateExit();
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnStateEnter();
        }
    }

    private void Update()
    {
        currentState.OnUpdate();
        StateChanges();
    }
}


public class EnemyController : MonoBehaviour
{

    public GameObject player;

    public EnemyState curState = EnemyState.Idle;

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

    public bool inCollisionRange = false;

    public bool coolDownAttack = false;

    private bool chooseDir = false;

    public bool notInRoom = false;

    public bool dealContactDamage = true;

    public GameObject bulletPrefab;

    public Rigidbody2D rb;

    public bool isFlying = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();
        curState = EnemyState.Idle;
        rb.freezeRotation = true;
        inCollisionRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        ContactDamage();
    }

    public bool IsPlayerInRange()
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
    
    public void MoveForward()
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

    public void SlowDown()
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
   

    //Verarbeitet den Angriffscooldown
    public IEnumerator CoolDown()
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

    public void ContactDamage()
    {
        if (dealContactDamage && inCollisionRange)
        {
            GameController.DamagePlayer(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inCollisionRange = true;
            ContactDamage();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inCollisionRange = false;
    }
}
