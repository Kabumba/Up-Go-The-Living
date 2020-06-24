using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spectral : BulletEffect
{
    // Start is called before the first frame update
    void Start()
    {
        defaultObstacleHit = false;
    }

    public override void OnInstantiate()
    {
        Color temp = GetComponent<SpriteRenderer>().color;
        temp.a = 0.6f;
        GetComponent<SpriteRenderer>().color = temp;
    }
}
