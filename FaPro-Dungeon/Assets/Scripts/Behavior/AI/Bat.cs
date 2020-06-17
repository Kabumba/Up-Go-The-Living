using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : AI
{
    public float dashSpeed;

    public float dashTime;

    public override void StateChanges()
    {
        if (Vector3.Distance(transform.position, character.player.transform.position) <= character.attackRange)
        {
            SetState(new Dash(character, Target.Player, dashSpeed, dashTime));
        }
        else
        {
            SetState(new Follow(character, character.player));
        }
    }
}
