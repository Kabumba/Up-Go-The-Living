using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : AI
{
    public float dashSpeed;

    public float dashTime;

    private float lastDashtime;

    public override void StateChanges()
    {
        if (Vector3.Distance(transform.position, character.player.transform.position) <= character.attackRange)
        {

            if (Time.time >= lastDashtime + dashTime)
            {
                SetState(new Dash(character, Target.Player, dashSpeed, dashTime));
                lastDashtime = Time.time;
            }

        }
        else
        {
            if (Time.time >= lastDashtime + dashTime)
            {
                SetState(new Follow(character, character.player));
            }
        }
    }
}
