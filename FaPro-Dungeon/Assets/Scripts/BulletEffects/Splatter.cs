using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splatter : BulletEffect
{
    private void Start()
    {
        isSplatter = true;
    }
    public override void OnDestroy()
    {
        if (!gameObject.name.Equals("BulletEffects"))
        {
            foreach (BulletShooter bs in gameObject.GetComponentsInChildren<BulletShooter>())
            {
                bs.SplatterShoot();
            }
        }
    }

    public override void OnInstantiate()
    {
        ShooterController referenceSc = GameObject.Find("Player").GetComponent<ShooterController>();
        ShooterController newSc = gameObject.AddComponent<ShooterController>();
        newSc.bulletSpeed = referenceSc.bulletSpeed;
        newSc.fireDelay = referenceSc.fireDelay;
        newSc.velocityAddedToBullet = referenceSc.velocityAddedToBullet;
        newSc.bulletShooters = new List<BulletShooter>();
        BulletShooter bs1 = Instantiate(GameObject.Find("Player").GetComponent<ShooterController>().bulletShooters[0], new Vector3(gameObject.transform.position.x - 0.35f, gameObject.transform.position.y + 0.35f, 0f), Quaternion.Euler(0, 0, 45));
        BulletShooter bs2 = Instantiate(GameObject.Find("Player").GetComponent<ShooterController>().bulletShooters[0], new Vector3(gameObject.transform.position.x + 0.35f, gameObject.transform.position.y + 0.35f, 0f), Quaternion.Euler(0, 0, -45));
        BulletShooter bs3 = Instantiate(GameObject.Find("Player").GetComponent<ShooterController>().bulletShooters[0], new Vector3(gameObject.transform.position.x + 0.35f, gameObject.transform.position.y - 0.35f, 0f), Quaternion.Euler(0, 0, -135));
        BulletShooter bs4 = Instantiate(GameObject.Find("Player").GetComponent<ShooterController>().bulletShooters[0], new Vector3(gameObject.transform.position.x - 0.35f, gameObject.transform.position.y - 0.35f, 0f), Quaternion.Euler(0, 0, 135));
        bs1.transform.parent = gameObject.transform;
        bs2.transform.parent = gameObject.transform;
        bs3.transform.parent = gameObject.transform;
        bs4.transform.parent = gameObject.transform;
        gameObject.GetComponent<ShooterController>().bulletPrefab = Resources.Load<GameObject>("Prefabs/Projectiles/SplatterBullet");
        gameObject.GetComponent<ShooterController>().bulletShooters.Clear();
        gameObject.GetComponent<ShooterController>().Add(bs1);
        gameObject.GetComponent<ShooterController>().Add(bs2);
        gameObject.GetComponent<ShooterController>().Add(bs3);
        gameObject.GetComponent<ShooterController>().Add(bs4);
    }
}
