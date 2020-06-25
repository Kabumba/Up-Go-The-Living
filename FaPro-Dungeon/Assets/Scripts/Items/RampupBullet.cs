using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampupBullet : Item
{
    public override void OnPickup()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        ShooterController shc = player.GetComponent<ShooterController>();
        RampupDamage rampupDamage = GameObject.Find("BulletEffects").AddComponent<RampupDamage>() as RampupDamage;
        shc.AddBulletEffectToAll(rampupDamage);
    }
}
