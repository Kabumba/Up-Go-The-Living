using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


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

    public string currentStateName;

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
        currentStateName = currentState.name;

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

    public float health = 10;

    public float maxHealth = 10;

    public float range;

    public float attackRange;

    public bool isDead = false;

    public bool dealContactDamage = true;

    public Rigidbody2D rb;

    public bool isFlying = false;

    public static int count = 0;

    public MovementController mvc;

    public ShooterController shc;

    public bool isBoss;

    public HealthBar healthBar;

    
    public SpriteRenderer GetSpriteRenderer()
    {
        if(GetComponents<SpriteRenderer>().Length == 0)
        {
            return GetComponentInChildren<SpriteRenderer>();
        }
        return GetComponent<SpriteRenderer>();
    }

    public void ApplyStatusEffect<effect>() where effect : EnemyEffect
    {
        EnemyEffect se = gameObject.AddComponent<effect>();
        se.OnApply();
    }

    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();
        mvc = gameObject.GetComponent<MovementController>();
        rb.freezeRotation = true;
        shc = gameObject.GetComponent<ShooterController>();
        if (isBoss)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
    }

    public void Awake()
    {
        count++;
    }

    public bool IsPlayerInRange()
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }
    
    public void Death()
    {
        if (!isDead)
        {
            isDead = true;
            count--;
            foreach (EnemyEffect de in gameObject.GetComponents<EnemyEffect>())
            {
                de.OnDeath();
            }
            Destroy(gameObject);
        }
    }

    public void DamageEnemy(float damage)
    {
        health = Mathf.Max(0, health - damage);
        if (isBoss)
        {
            healthBar.SetHealth(health);
        }
        if (health <= 0)
        {
            Death();
        }
    }

    public void ContactDamage()
    {
        if (dealContactDamage)
        {
            GameController.DamagePlayer(1);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ContactDamage();
        }
    }
}
