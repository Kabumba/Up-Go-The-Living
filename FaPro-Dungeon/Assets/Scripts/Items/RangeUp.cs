using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeUp : Item
{
    public override void OnPickup()
    {
        GameController.AddRange(4f);
    }
}
