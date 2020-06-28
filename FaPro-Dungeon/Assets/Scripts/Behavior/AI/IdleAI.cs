﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAI : AI
{
    public override void StateChanges()
    {
        SetState(new Idle(character));
    }
}
