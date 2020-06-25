using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUp : Item
{
    public override void OnPickup()
    {
        GameController.AddDamage(1);
    }
}