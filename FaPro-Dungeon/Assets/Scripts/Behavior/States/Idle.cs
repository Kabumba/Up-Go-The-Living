﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{
    public  Idle(EnemyController character) : base(character)
    {
        name = "Idle";
    }

    public override void OnStateEnter()
    {
        character.rb.velocity = new Vector2(0, 0);
    }

    public override void OnUpdate()
    {
        character.mvc.SlowDown();
    }
}
