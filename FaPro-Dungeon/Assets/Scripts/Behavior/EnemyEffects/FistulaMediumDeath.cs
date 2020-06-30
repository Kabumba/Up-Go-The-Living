using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistulaMediumDeath : EnemyEffect
{
    public GameObject prefab;

    public override void OnDeath()
    {
        Room currentRoom = transform.GetComponentInParent<Room>();
        GameObject x = GameObject.Instantiate(prefab, gameObject.transform.position, gameObject.transform.rotation);
        GameObject y = GameObject.Instantiate(prefab, gameObject.transform.position, gameObject.transform.rotation);

        x.transform.parent = currentRoom.transform;
        y.transform.parent = currentRoom.transform;

        x.transform.Rotate(new Vector3(0, 0, 90));
        y.transform.Rotate(new Vector3(0, 0, -90));

    }
}
