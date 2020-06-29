using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncy : BulletEffect
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        MovementController mvc = GetComponent<MovementController>();
        mvc.rebound(collision);
        GameObject parent = transform.parent.gameObject;
        Vector2 rotation = mvc.GetRotation();
        MovementController pmvc = parent.GetComponent<MovementController>();
        pmvc.SetRotation(rotation);
        parent.GetComponent<Rigidbody2D>().velocity = rotation * pmvc.speed;
        GetComponent<Rigidbody2D>().velocity = parent.GetComponent<Rigidbody2D>().velocity;
    }

    private void Awake()
    {
        defaultObstacleHit = false;
        defaultDestroyOnEnemyHit = false;
    }

    public override void OnInstantiate()
    {
        GameObject collHelp = Instantiate(gameObject, gameObject.transform);
        collHelp.AddComponent<BounceHelper>();
        collHelp.transform.parent = transform;
        collHelp.transform.position = transform.position;
        collHelp.transform.rotation = transform.rotation;
        collHelp.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
        Destroy(collHelp.GetComponent<BulletController>());
        Destroy(collHelp.GetComponent<SpriteRenderer>());
        collHelp.transform.localScale = new Vector2(1, 1);
        collHelp.layer = 13; //BulletHelper
        collHelp.GetComponent<Collider2D>().isTrigger = false;
    }
}
