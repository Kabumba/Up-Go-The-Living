using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawn : MonoBehaviour
{
    private List<EnemyController> enemies;
    public List<Transform> enemyPos = new List<Transform>();
    public Tilemap closedDoorTilemap;
    public Transform dropPos;
    public Transform stairsPos;

    // Start is called before the first frame update
    void Start()
    {
        closedDoorTilemap = gameObject.GetComponentInParent<Room>().GetTileMap();
    }

    private void Update()
    {
        if (EnemyController.count == 0)
        {
            GameController.AddDamage(-GameController.damageThroughRage);
            GameController.damageThroughRage = 0f;
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
            EnemyController.count = 0;
            EnemySpawner();
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void EnemySpawner()
    {
        Room room = GetComponentInParent<Room>();
        foreach (Transform ep in enemyPos.ToList<Transform>())
        {
            EnemySpecifics enemySpecific = ep.GetComponent<EnemySpecifics>();
            enemies = enemySpecific.enemies;
            int e = UnityEngine.Random.Range(0, enemies.Count);
            EnemyController enemy = Instantiate(enemies[e], ep.position, ep.rotation);
            enemy.transform.parent = room.transform;
            enemy.ApplyStatusEffect<StartOfRoom>();
            enemyPos.Remove(ep);
        }
    }

    public Vector3Int ToInt3(Vector3 v)
    {
        return new Vector3Int((int)v.x, (int)v.y, (int)v.z);
    }
}
