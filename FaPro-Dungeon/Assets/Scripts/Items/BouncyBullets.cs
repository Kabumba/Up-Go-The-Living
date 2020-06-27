using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBullets : Item
{
    public override void OnPickup()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        ShooterController shc = player.GetComponent<ShooterController>();
        Bouncy bouncy = GameObject.Find("BulletEffects").AddComponent<Bouncy>() as Bouncy;
        shc.AddBulletEffectToAll(bouncy);
    }
}
