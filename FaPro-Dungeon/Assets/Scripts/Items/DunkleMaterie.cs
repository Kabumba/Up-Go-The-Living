using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DunkleMaterie : Item
{
    public override void OnPickup()
    {
        GameController.ChangeFireDelay(new FireDelayDown());

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        ShooterController shc = player.GetComponent<ShooterController>();
        Fear fear = GameObject.Find("BulletEffects").AddComponent<Fear>() as Fear;
        shc.AddBulletEffectToAll(fear);
    }

    public class FireDelayDown : Statdecorator
    {
        public override float GetStat()
        {
            return next.GetStat() - 1;
        }
    }
}
