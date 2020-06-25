using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FRateUp : Item
{
    public override void OnPickup()
    {
        GameController.ChangeFireRate(new FireRateChange());
    }

    private class FireRateChange : Statdecorator
    {
        public override float GetStat()
        {
            return next.GetStat() + 0.7f;
        }
    }
}
