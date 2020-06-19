using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnHeart : DeathEvent
{
    public Item heartPrefab;
    private Transform dropPos;
    private float dropRate = 0.5f;
    public override void OnDeath()
    {
        Room currentRoom = transform.GetComponentInParent<Room>();
        if (EnemyController.count == 0)
        {

            this.dropPos = transform.parent.GetComponentInChildren<EnemySpawn>().dropPos;
            if (Random.Range(0f, 1f) > dropRate)
            {
                Item item = Instantiate(heartPrefab, dropPos.transform.position, dropPos.transform.rotation);
                item.transform.parent = currentRoom.transform;
            }
        }
    }
}
