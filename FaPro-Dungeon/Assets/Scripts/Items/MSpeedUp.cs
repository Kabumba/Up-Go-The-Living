using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSpeedUp : Item
{
    public override void OnPickup()
    {
        GameController.ChangeMoveSpeed(0.5f);
    }
}
