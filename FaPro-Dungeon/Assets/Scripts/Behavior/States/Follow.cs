using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : State
{
    private GameObject target;

    public Follow(EnemyController character, GameObject target) : base(character)
    {
        name = "Follow";
        this.target = target;
    }

    public override void OnUpdate()
    {
        character.rb.rotation = Vector2.SignedAngle(new Vector2(1, 0), target.transform.position - character.transform.position);
        character.MoveForward();
    }
}
