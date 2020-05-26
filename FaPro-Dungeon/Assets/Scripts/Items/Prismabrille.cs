using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Prismabrille : Item
{
    public override void OnPickup()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerController pc = player.GetComponent<PlayerController>();
        foreach(BulletShooter bs in pc.bulletShooters)
        {
            GameObject.Destroy(bs.gameObject);
        }
        pc.bulletShooters = new List<BulletShooter>();

        GameObject left = new GameObject("Bulletshooter left");
        GameObject right = new GameObject("Bulletshooter right");

        left.transform.parent = player.transform;
        right.transform.parent = player.transform;

        left.transform.position = new Vector3(player.transform.position.x + -0.25f, player.transform.position.y + 0.433f, 0f);
        right.transform.position = new Vector3(player.transform.position.x + 0.25f, player.transform.position.y + 0.433f, 0f);

        left.transform.rotation = Quaternion.Euler(0, 0, 45);
        right.transform.rotation = Quaternion.Euler(0, 0, -45);

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

