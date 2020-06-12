using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{
    bool idle;

    public  Idle(EnemyController character) : base(character)
    {
        name = "Idle";
        idle = false;
    }

    public override void OnUpdate()
    {
        if (!idle)
        {
            character.rb.velocity = new Vector2(0, 0);
        }
    }
}
