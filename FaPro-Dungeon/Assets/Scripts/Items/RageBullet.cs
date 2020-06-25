using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageBullet : Item
{
    public override void OnPickup()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        ShooterController sc = player.GetComponent<ShooterController>();
        Rage rage = GameObject.Find("BulletEffects").AddComponent<Rage>() as Rage;
        sc.AddBulletEffectToAll(rage);
    }
}
