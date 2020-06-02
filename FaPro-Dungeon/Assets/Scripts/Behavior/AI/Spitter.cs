using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitter : AI
{
    public override void StateChanges()
    {
        if (character.IsPlayerInRange())
        {
            if (Vector3.Distance(transform.position, character.player.transform.position) <= character.attackRange)
            {
                SetState(new RangeAttack(character));
            }
            else
            {
                SetState(new Follow(character, character.player));
            }
        }
        else
        {
            SetState(new Wander(character));
        }
    }
}
