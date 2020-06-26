using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonDarts : Item
{
    public override void OnPickup()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        ShooterController shc = player.GetComponent<ShooterController>();
        Poison poison = GameObject.Find("BulletEffects").AddComponent<Poison>() as Poison;
        shc.AddBulletEffectToAll(poison);
    }
}
