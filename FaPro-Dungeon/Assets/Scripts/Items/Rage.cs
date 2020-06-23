using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rage : Item
{
    public GameObject rageBullet;
    public override void OnPickup()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        ShooterController sc = player.GetComponent<ShooterController>();
        BulletEffect rageEffect = rageBullet.GetComponent<BulletEffect>();
        foreach(BulletShooter bs in sc.bulletShooters)
        {
            bs.bulletEffects.Add(rageEffect);
        }
    }
}
