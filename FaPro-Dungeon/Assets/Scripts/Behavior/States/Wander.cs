using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : State
{

    private bool chooseDir = false;
    private MovementController mvc;
    private ShooterController shc;

    public Wander(EnemyController character, bool shoot) : base(character)
    {
        name = "Wander";
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
        mvc.MoveForward();
    }

    private IEnumerator ChooseDirection()
    {
        chooseDir = true;
        yield return new WaitForSeconds(Random.Range(0.2f, 1f)); //wählt in zufälligen Abständen neue Richtung //wählt zufällige Richtung
        character.rb.rotation = Random.Range(0f, 360f);
        chooseDir = false;
    }
}
