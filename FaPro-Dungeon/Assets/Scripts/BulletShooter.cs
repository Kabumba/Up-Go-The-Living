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

    public int barrageCount = 1;

    public float barrageTimeDelay = 0;

    public List<BulletEffect> bulletEffects;

    public void Shoot()
    {
        if (Time.time > lastFireTime + fireTimeDelay)
        {
            if (lastFireShot >= fireShotDelay + fireShotOffset)
            {

                StartCoroutine(Barrage());
                lastFireShot = fireShotOffset;
            }
            else
            {
                lastFireShot++;
            }
            lastFireTime = Time.time;
        }
    }

    private IEnumerator Barrage()
    {
        for (int i = 0; i < barrageCount; i++)
        {
            float rand = Random.Range(0f, 1);
            if (rand < fireChance)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
                if (bulletEffects != null)
                {
                    foreach (BulletEffect be in bulletEffects)
                    {
                        bullet.AddComponent(be.GetType());
                    }
                }
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
                    transform.up.x * shooterController.bulletSpeed + shooterController.velocityAddedToBullet * shooterController.rb.velocity.x,
                    transform.up.y * shooterController.bulletSpeed + shooterController.velocityAddedToBullet * shooterController.rb.velocity.y,
                    0
                    );
                bullet.GetComponent<BulletController>().mvc.speed = shooterController.bulletSpeed;
                foreach(BulletEffect be in bullet.GetComponents<BulletEffect>())
                {
                    be.OnInstantiate();
                }
                GameController.OnFireItems();
            }
            if (i + 1 < barrageCount)
            {
                yield return new WaitForSeconds(barrageTimeDelay);
            }
        }
    }

    public void setFireShotDelay(int fireShotDelay)
    {
        this.fireShotDelay = fireShotDelay;
    }
}

