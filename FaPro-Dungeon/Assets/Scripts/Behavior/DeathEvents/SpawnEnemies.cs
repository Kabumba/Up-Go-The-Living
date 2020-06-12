using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : DeathEvent
{
    public GameObject enemyPrefab;

    public int number;

    public override void OnDeath()
    {
        for(int i=0; i < number; i++)
        {
            GameObject.Instantiate(enemyPrefab, gameObject.transform.position, gameObject.transform.rotation);
        }
    }
}
