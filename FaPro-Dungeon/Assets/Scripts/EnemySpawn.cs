using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemies;
    public List<Transform> enemyPos = new List<Transform>();
    public Tilemap closedDoorTilemap;
    // Start is called before the first frame update
    void Start()
    {
        closedDoorTilemap = gameObject.GetComponentInParent<Room>().GetTileMap();
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
            closedDoorTilemap.GetComponent<TilemapRenderer>().enabled = false;
        }
        
    }

    IEnumerator CloseDoorRoutine()
    {
        yield return new WaitForSeconds(0.2f);
        Room room = GetComponentInParent<Room>();
        foreach(Door door in room.GetComponentsInChildren<Door>())
        {
            door.doorCollider.SetActive(true);
            closedDoorTilemap.GetComponent<TilemapRenderer>().enabled = true;
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

    public Vector3Int ToInt3(Vector3 v)
    {
        return new Vector3Int((int)v.x, (int)v.y, (int)v.z);
    }
}
