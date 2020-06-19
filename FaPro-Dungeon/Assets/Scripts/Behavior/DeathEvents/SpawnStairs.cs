using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStairs : DeathEvent
{
    public override void OnDeath()
    {
        Room room = gameObject.GetComponentInParent<Room>();
    }
}
