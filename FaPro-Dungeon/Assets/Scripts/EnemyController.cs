﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Wander,
    Follow,
    Die,
    Attack,
};

public enum EnemyType
{
    Melee,
    Ranged,
};


public class EnemyController : MonoBehaviour
{

    GameObject player;

    public EnemyState currentState = EnemyState.Wander;

    public EnemyType enemyType;

    public float range = 5;

    public float speed = 3;

    public float attackRange;

    public float coolDown;

    private bool coolDownAttack = false;

    private bool chooseDir = false;

    private bool dead = false;

    public GameObject bulletPrefab;

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
            case (EnemyState.Attack):
                Attack();
                break;
        }

        if(IsPlayerInRange() && currentState != EnemyState.Die)
        {
            currentState = EnemyState.Follow;
        } else if (!IsPlayerInRange() && currentState != EnemyState.Die)
        {
            currentState = EnemyState.Wander;
        }
        if(Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            currentState = EnemyState.Attack;
        }
    }

    private bool IsPlayerInRange()
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    private IEnumerator ChooseDirection()
    {
        chooseDir = true;
        yield return new WaitForSeconds(Random.Range(1f, 3f)); //wählt in zufälligen Abständen neue Richtung
        Vector3 randomDir = new Vector3(0, 0, Random.Range(0, 360)); //wählt zufällige Richtung
        Quaternion nextRotation = Quaternion.Euler(randomDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f)); //dreht sich über eine zufällige Zeit in diese Richtung
        chooseDir = false;
    }

    //bewegt sich ziellos herum
    void Wander()
    {
        if (!chooseDir)
        {
            StartCoroutine(ChooseDirection());
        }
        transform.position += -transform.right * speed * Time.deltaTime;
        if (IsPlayerInRange())
        {
            currentState = EnemyState.Follow;
        }
    }

    //Bewegt sich auf den spieler zu
    void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
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
        Destroy(gameObject);
    }
}
