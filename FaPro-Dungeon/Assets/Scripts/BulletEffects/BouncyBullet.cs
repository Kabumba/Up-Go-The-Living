using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBullet : BulletEffect
{
    public override void OnObstacleHit(Collider2D collision)
    {
        
    }

    private void Awake()
    {
        defaultObstacleHit = false;
        
    }
}
