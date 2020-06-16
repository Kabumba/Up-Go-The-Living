using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUp : Item
{
    public override void OnPickup()
    {
        GameController.ChangeDamage(GameController.Damage);
    }
}
