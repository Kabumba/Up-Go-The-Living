using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class SpawnStairs : DeathEvent
{
    public GameObject stairsPrefab;
    public override void OnDeath()
    {
        Room room = gameObject.GetComponentInParent<Room>();
        if(EnemyController.count == 0)
        {
            Transform stairsPos = transform.parent.GetComponentInChildren<EnemySpawn>().stairsPos;
            GameObject stairs = Instantiate(stairsPrefab, stairsPos.position, stairsPos.rotation);
            stairs.transform.parent = room.transform;
            Transform dropPos = transform.parent.GetComponentInChildren<EnemySpawn>().dropPos;
            List<Item> items = dropPos.GetComponent<DropSpecifics>().items;
            int random = Random.Range(0, items.Count);
            Item bossDrop = Instantiate(items[random], dropPos.position, dropPos.rotation);
            bossDrop.transform.parent = room.transform;
        }
    }
}
