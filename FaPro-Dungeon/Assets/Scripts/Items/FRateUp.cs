using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FRateUp : Item
{
    public override void OnPickup()
    {
        if (GameController.FireRate > 0.1)
        {
            GameController.ChangeFireRate(-GameController.FireRate / 5);
        }
    }
}
