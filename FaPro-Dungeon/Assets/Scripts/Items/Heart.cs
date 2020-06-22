using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Item
{
    public override void OnPickup()
    {
        if (GameController.Health < GameController.MaxHealth)
        {
            GameController.HealPlayer(2);
            Destroy(gameObject);
        }
    }
}
