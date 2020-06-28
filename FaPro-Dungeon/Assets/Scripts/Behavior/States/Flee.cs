using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : State
{
    private GameObject target;
    private MovementController mvc;

    public Flee(EnemyController character, GameObject target) : base(character)
    {
        name = "Flee";
        this.target = target;
        mvc = character.mvc;
    }

    public override void OnUpdate()
    {
        mvc.RotateTowards(character.player);
        mvc.SetRotation(-mvc.GetRotation());
        mvc.MoveForward();
    }
}

