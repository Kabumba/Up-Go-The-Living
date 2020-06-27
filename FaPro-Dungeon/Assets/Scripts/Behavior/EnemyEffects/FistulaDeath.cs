using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistulaDeath : EnemyEffect
{
    public GameObject prefab;

    public override void OnDeath()
    {
        Room currentRoom = transform.GetComponentInParent<Room>();
        GameObject enemy1 = GameObject.Instantiate(prefab, gameObject.transform.position, Quaternion.Euler(0, 0, 45));
        GameObject enemy2 = GameObject.Instantiate(prefab, gameObject.transform.position, Quaternion.Euler(0, 0, -45));
        GameObject enemy3 = GameObject.Instantiate(prefab, gameObject.transform.position, Quaternion.Euler(0, 0, 135));
        GameObject enemy4 = GameObject.Instantiate(prefab, gameObject.transform.position, Quaternion.Euler(0, 0, -135));
        enemy1.transform.parent = currentRoom.transform;
        enemy2.transform.parent = currentRoom.transform;
        enemy3.transform.parent = currentRoom.transform;
        enemy4.transform.parent = currentRoom.transform;
    }

    private void Start()
    {
        gameObject.transform.rotation = Quaternion.Euler(0, 0, -45);
    }
}
