using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Wander,
    Follow,
    Die,
};

public class EnemyController : MonoBehaviour
{

    GameObject player;

    public EnemyState currentState = EnemyState.Wander;

    public float range = 5;

    public float speed = 3;

    private bool chooseDir = false;

    private bool dead = false;

    private Vector3 randomDir;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
        }

        if(IsPlayerInRange() && currentState != EnemyState.Die)
        {
            currentState = EnemyState.Follow;
        } else if (!IsPlayerInRange() && currentState != EnemyState.Die)
        {
            currentState = EnemyState.Wander;
        }
    }

    private bool IsPlayerInRange()
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    private IEnumerator ChooseDirection()
    {
        chooseDir = true;
        yield return new WaitForSeconds(Random.Range(2f, 8f)); //wählt in zufälligen Abständen neue Richtung
        Vector3 randomDir = new Vector3(0, 0, Random.Range(0, 360)); //wählt zufällige Richtung
        Quaternion nextRotation = Quaternion.Euler(randomDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f)); //dreht sich über eine zufällige Zeit in diese Richtung
        chooseDir = false;
    }

    void Wander()
    {
        if (!chooseDir)
        {
            StartCoroutine(ChooseDirection());
        }
        transform.position = -transform.right * speed * Time.deltaTime;
        if (IsPlayerInRange())
        {
            currentState = EnemyState.Follow;
        }
    }

    void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
