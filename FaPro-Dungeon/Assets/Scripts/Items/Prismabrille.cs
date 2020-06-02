using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Prismabrille : Item
{
    public override void OnPickup()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerController pc = player.GetComponent<PlayerController>();
        List<GameObject> toRemove = new List<GameObject>();
        foreach (BulletShooter bs in pc.bulletShooters)
        {
            if (bs.gameObject.name.StartsWith("Main"))
            {
                toRemove.Add(bs.gameObject);
            }
        }
        foreach (GameObject bs in toRemove)
        {
            pc.bulletShooters.Remove(bs.GetComponent<BulletShooter>());
            GameObject.Destroy(bs);
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

        pc.bulletShooters.Add(leftShoot);
        pc.bulletShooters.Add(rightShoot);

        pc.InitializeBulletshooters();
    }
}

