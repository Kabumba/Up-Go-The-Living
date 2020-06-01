using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : AI
{
    public override void StateChanges()
    {
        if (!character.notInRoom)
        {
            SetState(new Follow(character, character.player));
        }
        else
        {
            SetState(new Idle(character));
        }
    }
}
