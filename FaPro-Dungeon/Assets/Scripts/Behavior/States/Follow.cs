using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : State
{
    private GameObject target;
    private MovementController mvc;

    public Follow(EnemyController character, GameObject target) : base(character)
    {
        name = "Follow";
        this.target = target;
        mvc = character.mvc;
    }

    public override void OnUpdate()
    {
        character.rb.rotation = Vector2.SignedAngle(new Vector2(0, 1), target.transform.position - character.transform.position);
        mvc.MoveForward();
    }
}
