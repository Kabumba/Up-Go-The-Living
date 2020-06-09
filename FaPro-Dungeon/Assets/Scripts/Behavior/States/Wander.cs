using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : State
{

    private bool chooseDir = false;
    private MovementController mvc;

    public Wander(EnemyController character) : base(character)
    {
        name = "Wander";
        mvc = character.mvc;
    }

    public override void OnUpdate()
    {
        if (!chooseDir)
        {
            character.StartCoroutine(ChooseDirection());
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
