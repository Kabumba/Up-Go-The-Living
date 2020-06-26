using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBullet : Item
{
    public override void OnPickup()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        ShooterController shc = player.GetComponent<ShooterController>();
        Shield shield = GameObject.Find("BulletEffects").AddComponent<Shield>() as Shield;
        shc.AddBulletEffectToAll(shield);
    }
}
