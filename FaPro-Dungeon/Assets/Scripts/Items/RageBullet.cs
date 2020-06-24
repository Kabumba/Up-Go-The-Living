using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageBullet : Item
{
    public override void OnPickup()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        ShooterController shc = player.GetComponent<ShooterController>();
        shc.AddBulletEffectToAll(new Rage());
    }
}
