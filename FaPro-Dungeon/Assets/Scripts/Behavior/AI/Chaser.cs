using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : AI
{
    public override void StateChanges()
    {
        SetState(new Follow(character, character.player));
    }
}
