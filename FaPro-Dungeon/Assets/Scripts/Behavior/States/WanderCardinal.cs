using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderCardinal : State
{
    private bool chooseDir = false;
    private MovementController mvc;
    private ShooterController shc;

    public WanderCardinal(EnemyController character, bool shoot) : base(character)
    {
        name = "WanderCardinal";
        mvc = character.mvc;
        if (shoot)
        {
            shc = character.shc;
            name += "true";
        }
    }

    public override void OnUpdate()
    {
        if (!chooseDir)
        {
            character.StartCoroutine(ChooseDirection());
        }
        if (shc != null)
        {
            shc.TryShoot();
        }
        mvc.MoveForwardFixedSpeed();
    }

    private IEnumerator ChooseDirection()
    {
        chooseDir = true;
        character.rb.rotation = 90 * Mathf.RoundToInt(Random.Range(-1.5f, 2.5f));
        yield return new WaitForSeconds(Mathf.RoundToInt(Random.Range(0.5f, 3.5f)) / mvc.speed);
        chooseDir = false;
    }
}
