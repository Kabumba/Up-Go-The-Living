using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    private float lastFireTime;

    private int lastFireShot;

    public float fireTimeDelay;

    public int fireShotDelay;

    public PlayerController playerController;

    public int fireShotOffset;

    public float fireChance;

    public void Start()
    {
        playerController.bulletShooters.Add(this);
    }

    public void Shoot()
    {
        if (Time.time > lastFireTime + fireTimeDelay)
        {
            if (lastFireShot >= fireShotDelay + fireShotOffset)
            {
                float rand = Random.Range(0f, 1);
                if (rand < fireChance)
                {
                    GameObject bullet = Instantiate(playerController.bulletPrefab, transform.position, transform.rotation) as GameObject;
                    bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
                    bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
                        transform.up.x * playerController.bulletSpeed + playerController.velocityAddedToBullet * playerController.rb.velocity.x,
                        transform.up.y * playerController.bulletSpeed + playerController.velocityAddedToBullet * playerController.rb.velocity.y,
                        0
                        );
                }
                lastFireShot = fireShotOffset;
            }
            else
            {
                lastFireShot++;
            }
            lastFireTime = Time.time;
        }
    }
}
