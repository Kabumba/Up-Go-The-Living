using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatterShot : Item
{
    public override void OnPickup()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        ShooterController shc = player.GetComponent<ShooterController>();
        Splatter splatter = GameObject.Find("BulletEffects").AddComponent<Splatter>() as Splatter;
        shc.AddBulletEffectToAll(splatter);
    }
}
