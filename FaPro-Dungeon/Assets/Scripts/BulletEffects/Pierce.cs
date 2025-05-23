﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pierce : BulletEffect
{
    // Start is called before the first frame update
    void Start()
    {
        defaultKnockbackEnemy = false;
        defaultDestroyOnEnemyHit = false;
    }

    public override void OnInstantiate()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color temp = sr.color;
        sr.sprite = Resources.Load("Sprites/PierceProjectile", typeof(Sprite)) as Sprite;
        sr.color = temp;
        Destroy(GetComponent<CircleCollider2D>());
        gameObject.AddComponent(typeof(PolygonCollider2D));
        GetComponent<PolygonCollider2D>().isTrigger = true;
        GetComponent<MovementController>().rb = GetComponent<Rigidbody2D>();
        GetComponent<MovementController>().SetRotation(GetComponent<Rigidbody2D>().velocity);
    }
}