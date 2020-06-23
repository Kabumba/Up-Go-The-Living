using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleBullet : Item
{
    public override void OnPickup()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        ShooterController shc = player.GetComponent<ShooterController>();
        Quaternion rotationBefore = player.transform.rotation;
        foreach (BulletShooter bs in shc.bulletShooters)
        {
            bs.fireShotDelay = -1;
            bs.transform.Rotate(0, 0, 0);
        }
        player.transform.rotation = rotationBefore;
    }
}
