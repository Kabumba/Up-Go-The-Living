﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistulaMediumDeath : DeathEvent
{
    public GameObject prefab;

    public override void OnDeath()
    {
        GameObject x = GameObject.Instantiate(prefab, gameObject.transform.position, gameObject.transform.rotation);
        GameObject y = GameObject.Instantiate(prefab, gameObject.transform.position, gameObject.transform.rotation);

        x.transform.Rotate(new Vector3(0, 0, 90));
        y.transform.Rotate(new Vector3(0, 0, -90));
    }
}
