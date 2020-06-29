using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : Item
{
    public override void OnPickup()
    {
        GameObject.FindGameObjectWithTag("Player").layer = 14; //PlayerFlying
    }
}
