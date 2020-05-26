using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    private float lastFireTime;

    private int lastFireShot;

    public GameObject bulletPrefab;

    public float fireTimeDelay;

    public int fireShotDelay = 0;

    public PlayerController playerController;

    public int fireShotOffset = 0;

    public float fireChance = 1;


    public void Shoot()
    {
        if (Time.time > lastFireTime + fireTimeDelay)
        {
            if (lastFireShot >= fireShotDelay + fireShotOffset)
            {
                float rand = Random.Range(0f, 1);
                if (rand < fireChance)
                {
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
                    bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
                    bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
                        transform.up.x * playerController.bulletSpeed + playerController.velocityAddedToBullet * playerController.rb.velocity.x,
                        transform.up.y * playerController.bulletSpeed + playerController.velocityAddedToBullet * playerController.rb.velocity.y,
                        0
                        );
                    GameController.OnFireItems();
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
