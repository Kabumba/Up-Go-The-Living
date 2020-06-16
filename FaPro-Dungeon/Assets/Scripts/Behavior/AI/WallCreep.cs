using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCreep : AI
{
    private float lastshoot;

    public float wanderTime;

    public float shootTime;

    public override void StateChanges()
    {
        if (Time.time >= lastshoot + wanderTime)
        {
            if(Time.time <= lastshoot + wanderTime + shootTime)
            {
                SetState(new RangeAttack(character, Target.None));
            }
            else
            {
                lastshoot = Time.time;
                SetState(new SidewayFollow(character));
            }
        }
        else
        {
            SetState(new SidewayFollow(character));
        }
    }
}
