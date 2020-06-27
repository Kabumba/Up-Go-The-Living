using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistulaDeath : DeathEvent
{
    public GameObject prefab;

    public override void OnDeath()
    {
        Room currRoom = gameObject.GetComponentInParent<Room>();

        GameObject fistulaPrefab1 = Instantiate(prefab, gameObject.transform.position, Quaternion.Euler(0, 0, 45));
        GameObject fistulaPrefab2 = Instantiate(prefab, gameObject.transform.position, Quaternion.Euler(0, 0, -45));
        GameObject fistulaPrefab3 = Instantiate(prefab, gameObject.transform.position, Quaternion.Euler(0, 0, 135));
        GameObject fistulaPrefab4 = Instantiate(prefab, gameObject.transform.position, Quaternion.Euler(0, 0, -135));

        fistulaPrefab1.transform.parent = currRoom.transform;
        fistulaPrefab2.transform.parent = currRoom.transform;
        fistulaPrefab3.transform.parent = currRoom.transform;
        fistulaPrefab4.transform.parent = currRoom.transform;
    }

    private void Start()
    {
        gameObject.transform.rotation = Quaternion.Euler(0, 0, -45);
    }
}
