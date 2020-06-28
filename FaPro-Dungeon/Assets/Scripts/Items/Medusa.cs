using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medusa : Item
{
    public override void OnPickup()
    {
        GameController.AddRange(2);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        ShooterController shc = player.GetComponent<ShooterController>();
        Petrify petrify = GameObject.Find("BulletEffects").AddComponent<Petrify>() as Petrify;
        shc.AddBulletEffectToAll(petrify);
    }
}
