using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidFire : Item
{
    public override void OnPickup()
    {
        GameController.ChangeFireDelay(new FireDelayChange());
        GameController.MultiplyDamageMultiplier(0.2f);
    }

    private class FireDelayChange : Statdecorator
    {
        public override float GetStat()
        {
            return (next.GetStat()/4)-2;
        }
    }
}
