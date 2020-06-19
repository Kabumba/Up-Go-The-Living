using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidewayFollow : State
{
    private MovementController mvc;
    private ShooterController shc;
    private Vector2 normal;
    private Vector2 left;
    private Vector2 right;

    public SidewayFollow(EnemyController character) : base(character)
    {
        name = "SidewayFollow";
        mvc = character.mvc;
        shc = character.shc;
        normal = mvc.GetRotation();
        right = new Vector2(normal.y, -normal.x);
        left = new Vector2(-normal.y, normal.x);
    }

    public override void OnUpdate()
    {
        float angle = Vector2.SignedAngle(normal, character.player.transform.position - character.transform.position);
        if (angle < -5)
        {
            mvc.SetRotation(right);
            mvc.MoveForward();
        }
        else
        {
            if (angle > 5)
            {
                mvc.SetRotation(left);
                mvc.MoveForward();
            }
            else
            {
                mvc.SlowDown();
            }
        }
        mvc.SetRotation(normal);
    }
}
