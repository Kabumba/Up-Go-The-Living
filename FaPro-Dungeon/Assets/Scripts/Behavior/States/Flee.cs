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
        character.rb.rotation = Vector2.SignedAngle(new Vector2(-1, 0), target.transform.position - character.transform.position);
        mvc.MoveForward();
    }
}

