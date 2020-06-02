using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemies;
    public List<Transform> enemyPos = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (EnemyController.count == 0)
        {
            StartCoroutine(OpenDoorRoutine());
        }
        else
        {
            StartCoroutine(CloseDoorRoutine());
        }
    }

    IEnumerator OpenDoorRoutine()
    {
        yield return new WaitForSeconds(0.2f);
        Room room = GetComponentInParent<Room>();
        foreach(Door door in room.GetComponentsInChildren<Door>())
        {
            door.doorCollider.SetActive(false);
        }
        
    }

    IEnumerator CloseDoorRoutine()
    {
        yield return new WaitForSeconds(0.2f);
        Room room = GetComponentInParent<Room>();
        foreach(Door door in room.GetComponentsInChildren<Door>())
        {
            door.doorCollider.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            EnemySpawner();
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void EnemySpawner()
    {
        int ep = UnityEngine.Random.Range(1, enemyPos.Count);
        for (int i = 0; i < ep; i++)
        {
            int p = UnityEngine.Random.Range(0, enemyPos.Count);
            int e = UnityEngine.Random.Range(0, enemies.Length);
            Instantiate(enemies[e], enemyPos[p].position, enemyPos[p].rotation);
            enemyPos.RemoveAt(p);
        }
    }
}
