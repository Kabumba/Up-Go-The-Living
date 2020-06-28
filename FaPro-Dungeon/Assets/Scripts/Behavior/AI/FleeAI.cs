using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeAI : AI
{
    public override void StateChanges()
    {
        SetState(new Flee(character, character.player));
    }
}
