using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealthUp : Item
{
    public override void OnPickup()
    {
        GameController.AddMaxHealth(2);
        GameController.HealPlayer(2);
    }
}
