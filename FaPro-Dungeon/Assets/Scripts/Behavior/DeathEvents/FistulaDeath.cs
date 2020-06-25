using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistulaDeath : DeathEvent
{
    public GameObject prefab;

    public override void OnDeath()
    {
        GameObject.Instantiate(prefab, gameObject.transform.position, Quaternion.Euler(0, 0, 45));
        GameObject.Instantiate(prefab, gameObject.transform.position, Quaternion.Euler(0, 0, -45));
        GameObject.Instantiate(prefab, gameObject.transform.position, Quaternion.Euler(0, 0, 135));
        GameObject.Instantiate(prefab, gameObject.transform.position, Quaternion.Euler(0, 0, -135));
    }

    private void Start()
    {
        gameObject.transform.rotation = Quaternion.Euler(0, 0, -45);
    }
}
