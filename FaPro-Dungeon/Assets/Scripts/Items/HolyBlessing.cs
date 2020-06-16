using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyBlessing : Item
{
    public override void OnPickup()
    {
        GameController.ChangeRange(6f);
        GameController.ChangeFireRate(-GameController.FireRate / 3);
        GameController.ChangeMaxHealth(1);
        GameController.ChangeDamage(GameController.Damage);
        GameController.ChangeMoveSpeed(0.75f);
    }
}
