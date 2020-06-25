using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSpeedUp : Item
{
    public override void OnPickup()
    {
        GameController.AddMoveSpeed(0.5f);
    }
}
