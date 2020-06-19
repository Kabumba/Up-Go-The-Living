using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : State
{
    private MovementController mvc;
    private ShooterController shc;
    private Target target;

    public RangeAttack(EnemyController character, Target target) : base(character)
    {
        name = "RangeAttack";
        mvc = character.mvc;
        shc = character.shc;
        this.target = target;
    }

    public override void OnUpdate()
    {
        if (target == Target.Player)
        {
            mvc.RotateTowards(character.player);
        }
        shc.TryShoot();
        mvc.SlowDown();
    }
}
