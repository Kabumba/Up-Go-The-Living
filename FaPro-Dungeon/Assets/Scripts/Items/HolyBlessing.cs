using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyBlessing : Item
{
    public override void OnPickup()
    {
        GameController.AddRange(6f);
        GameController.ChangeFireRate(new FireRateChange());
        GameController.AddMaxHealth(1);
        GameController.AddDamage(1.5f);
        GameController.AddMoveSpeed(0.4f);
    }

    private class FireRateChange : Statdecorator
    {
        public override float GetStat()
        {
            return next.GetStat() + 0.3f;
        }
    }
}
