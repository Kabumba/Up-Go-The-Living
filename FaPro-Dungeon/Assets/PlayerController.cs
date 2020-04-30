﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    public GameObject bulletPrefab;

    public float bulletSpeed;

    private float lastFire;

    public float fireDelay;

    Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Inputs
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float shootHorizontal = Input.GetAxis("ShootHorizontal");
        float shootVertical = Input.GetAxis("ShootVertical");

        //basic Movement bei dem man sich diagonal schneller bewegt
        //rigidbody.velocity = new Vector3(horizontal * speed, vertical * speed, 0); 

        float unadjustedSpeed = Mathf.Sqrt(horizontal * horizontal + vertical * vertical);
        float speedfactor = unadjustedSpeed == 0 ? 0 : speed / unadjustedSpeed;
        rigidbody.velocity = new Vector3(horizontal * speedfactor, vertical * speedfactor, 0);

        //Schüsse nur Vertikal oder Horizontal
        if (Time.time > lastFire + fireDelay)
        {
            if (shootHorizontal != 0)
            {
                Shoot(shootHorizontal, 0);
            }
            else
                if (shootVertical != 0)
                {
                Shoot(0, shootVertical);
                }
        }
    }

    void Shoot(float x, float y)
    {
        GameObject bullet = Instantiate(bulletPrefab,transform.position,transform.rotation) as GameObject;
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        float fixedX = (x < 0) ? Mathf.Floor(x) : Mathf.Ceil(x);
        float fixedY = (y < 0) ? Mathf.Floor(y) : Mathf.Ceil(y);
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
            fixedX * bulletSpeed + rigidbody.velocity.x,
            fixedY * bulletSpeed + rigidbody.velocity.y,
            0
            );
        lastFire = Time.time;
    }
}
