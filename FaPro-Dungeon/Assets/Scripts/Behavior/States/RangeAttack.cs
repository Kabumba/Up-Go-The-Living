using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : State
{
    private MovementController mvc;
    private ShooterController shc;

    public RangeAttack(EnemyController character) : base(character)
    {
        name = "RangeAttack";
        mvc = character.mvc;
        shc = character.shc;
    }

    public override void OnUpdate()
    {
        mvc.RotateTowards(character.player);
        shc.TryShoot();
        mvc.SlowDown();
    }
}
