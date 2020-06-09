using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Prismabrille : Item
{
    public override void OnPickup()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        ShooterController shc = player.GetComponent<ShooterController>();
        List<GameObject> toRemove = new List<GameObject>();
        foreach (BulletShooter bs in shc.bulletShooters)
        {
            switch (bs.gameObject.name)
            {
                case ("Main Left"):
                    toRemove.Add(bs.gameObject);
                    break;
                case ("Main Right"):
                    toRemove.Add(bs.gameObject);
                    break;
            }
        }

        GameObject left = new GameObject("Main Left");
        GameObject right = new GameObject("Main Right");

        left.transform.parent = player.transform;
        right.transform.parent = player.transform;

        Quaternion rotationBefore = player.transform.rotation;
        player.transform.rotation = Quaternion.Euler(0, 0, 0);

        left.transform.position = new Vector3(player.transform.position.x + -0.25f, player.transform.position.y + 0.433f, 0f);
        right.transform.position = new Vector3(player.transform.position.x + 0.25f, player.transform.position.y + 0.433f, 0f);

        left.transform.rotation = Quaternion.Euler(0, 0, 45);
        right.transform.rotation = Quaternion.Euler(0, 0, -45);

        player.transform.rotation = rotationBefore;

        left.AddComponent<BulletShooter>();
        right.AddComponent<BulletShooter>();

        BulletShooter leftShoot = left.GetComponent<BulletShooter>();
        BulletShooter rightShoot = right.GetComponent<BulletShooter>();

        leftShoot.fireShotDelay = 1;
        rightShoot.fireShotDelay = 1;
        rightShoot.fireShotOffset = 1;

        shc.bulletShooters.Add(leftShoot);
        shc.bulletShooters.Add(rightShoot);


        foreach (GameObject bs in toRemove)
        {
            switch (bs.gameObject.name)
            {
                case ("Main Left"):
                    leftShoot.bulletEffects = bs.gameObject.GetComponent<BulletShooter>().bulletEffects;
                    break;
                case ("Main Right"):
                    rightShoot.bulletEffects = bs.gameObject.GetComponent<BulletShooter>().bulletEffects;
                    break;
            }
            shc.bulletShooters.Remove(bs.GetComponent<BulletShooter>());
            GameObject.Destroy(bs);
        }

        shc.InitializeBulletshooters();
    }
}

