using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BulletShooter : MonoBehaviour
{
    //Konvention: Die Namen der gameobjekte mit bulletshooter die den Hauptangriff darstellen beginnen mit Main

    private float lastFireTime;

    private int lastFireShot;

    public GameObject bulletPrefab;

    public float fireTimeDelay;

    public int fireShotDelay = 0;

    public ShooterController shooterController;

    public int fireShotOffset = 0;

    public float fireTimeOffset = 0;

    public float fireChance = 1;

    public List<BulletEffect> bulletEffects;

    public void Shoot()
    {
        if (Time.time > lastFireTime + fireTimeDelay)
        {
            StartCoroutine(Wait(fireTimeOffset));
            if (lastFireShot >= fireShotDelay + fireShotOffset)
            {
                float rand = Random.Range(0f, 1);
                if (rand < fireChance)
                {
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
                    if (bulletEffects != null)
                    {
                        foreach (BulletEffect be in bulletEffects)
                        {
                            bullet.GetComponent<BulletController>().bulletEffects.Add(be);
                        }
                    }
                    bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
                        transform.up.x * shooterController.bulletSpeed + shooterController.velocityAddedToBullet * shooterController.rb.velocity.x,
                        transform.up.y * shooterController.bulletSpeed + shooterController.velocityAddedToBullet * shooterController.rb.velocity.y,
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

    private IEnumerator Wait(float s)
    {
        yield return new WaitForSeconds(s);
    }

    public void setFireShotDelay(int fireShotDelay)
    {
        this.fireShotDelay = fireShotDelay;
    }
}

