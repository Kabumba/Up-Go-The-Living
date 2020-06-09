using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : State
{
    public float bulletSpeed = 7f;
    public float range = 30f;
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
        character.rb.rotation = Vector2.SignedAngle(new Vector2(0, 1), character.player.transform.position - character.transform.position);
        shc.TryShoot();
        mvc.SlowDown();
    }
}
