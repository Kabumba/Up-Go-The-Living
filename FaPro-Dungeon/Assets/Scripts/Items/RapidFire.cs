using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidFire : Item
{
    public override void OnPickup()
    {
        GameController.FireRate = 0.1f;
        GameController.ChangeDamage(-GameController.Damage / 2);
    }
}
