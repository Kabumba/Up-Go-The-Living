using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingShots : Item
{
    public override void OnPickup()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        ShooterController shc = player.GetComponent<ShooterController>();
        Pierce pierce = GameObject.Find("BulletEffects").AddComponent<Pierce>() as Pierce;
        shc.AddBulletEffectToAll(pierce);
    }
}
