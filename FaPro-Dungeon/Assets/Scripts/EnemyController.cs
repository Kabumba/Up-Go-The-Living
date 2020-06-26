using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public abstract class DeathEvent : MonoBehaviour
{
    public abstract void OnDeath();
}

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

    //private float maxHealth = 10;

    public float range;

    public float attackRange;

    public bool isDead = false;

    public bool dealContactDamage = true;

    public Rigidbody2D rb;

    public bool isFlying = false;

    public static int count = 0;

    public MovementController mvc;

    public ShooterController shc;

    public int poisonTicks = 0;

    public float poisonDamage = 1f;

    public bool isPoisoned;

    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();
        mvc = gameObject.GetComponent<MovementController>();
        rb.freezeRotation = true;
        shc = gameObject.GetComponent<ShooterController>();
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
            foreach (DeathEvent de in gameObject.GetComponents<DeathEvent>())
            {
                de.OnDeath();
            }
            Destroy(gameObject);
        }
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

    public void PoisonDamage()
    {
        if (!isPoisoned)
        {
            poisonTicks = 3;
            StartCoroutine("PoisonTicks");
        }
    }

    IEnumerator PoisonTicks()
    {
        isPoisoned = true;
        yield return new WaitForSeconds(1f);
        while (poisonTicks > 0) {
            health = Mathf.Max(0, health - poisonDamage);
            poisonTicks -= 1;
            Debug.Log("Tick");
            yield return new WaitForSeconds(1f);
        }
        isPoisoned = false;
    }
}
