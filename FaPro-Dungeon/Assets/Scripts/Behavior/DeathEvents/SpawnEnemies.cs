using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : DeathEvent
{
    public EnemyController enemyPrefab;

    public int number;

    public override void OnDeath()
    {
        for(int i=0; i < number; i++)
        {
            Room currentRoom = transform.GetComponentInParent<Room>();
            EnemyController enemy = Instantiate(enemyPrefab, gameObject.transform.position, gameObject.transform.rotation);
            enemy.transform.parent = currentRoom.transform;
        }
    }
}
