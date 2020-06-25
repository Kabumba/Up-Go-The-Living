using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fistula : AI
{
    public override void StateChanges()
    {
        SetState(new Forward(character));
    }
}
