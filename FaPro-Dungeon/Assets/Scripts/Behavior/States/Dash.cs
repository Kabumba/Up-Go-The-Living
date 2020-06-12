using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : State
{
    public float dashSpeed;
    public float dashTime;

    private MovementController mvc;
    private Target target;

    public Dash(EnemyController character, Target target, float dashSpeed, float dashTime) : base(character)
    {
        name = "Dash";
        this.dashTime = dashTime;
        this.dashSpeed = dashSpeed;
        mvc = character.mvc;
        this.target = target;
    }

    public override void OnUpdate()
    {
        if (target == Target.None)
        {
            character.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        }
        else
        {
            mvc.RotateTowards(character.player);
        }
        mvc.Dash(dashSpeed, dashTime);
    }
}
