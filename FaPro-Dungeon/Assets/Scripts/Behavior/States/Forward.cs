using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forward : State
{
    public Forward(EnemyController character) : base(character)
    {
        name = "Forward";
    }

    // Update is called once per frame
    public override void OnUpdate()
    {
        character.mvc.MoveForward();
    }
}
