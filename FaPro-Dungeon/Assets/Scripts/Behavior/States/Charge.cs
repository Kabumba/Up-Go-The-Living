using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : State
{
    private float chargeSpeed;
    private float nonChargeSpeed;
    private MovementController mvc;
    private ShooterController shc;

    public Charge(EnemyController character, float chargeSpeed) : base(character)
    {
        name = "Charge";
        mvc = character.mvc;
        this.chargeSpeed = chargeSpeed;
    }

    public override void OnUpdate()
    {
        mvc.MoveForward();
    }

    public override void OnStateEnter()
    {
        nonChargeSpeed = mvc.speed;
        mvc.speed = chargeSpeed;
    }

    public override void OnStateExit()
    {
        mvc.speed = nonChargeSpeed;
    }
}
