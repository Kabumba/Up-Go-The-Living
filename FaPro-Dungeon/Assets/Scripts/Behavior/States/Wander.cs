using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : State
{

    private bool chooseDir = false;

    public Wander(EnemyController character) : base(character)
    {
        name = "Wander";
    }

    public override void OnUpdate()
    {
        if (!chooseDir)
        {
            character.StartCoroutine(ChooseDirection());
        }
        character.MoveForward();
    }

    private IEnumerator ChooseDirection()
    {
        chooseDir = true;
        yield return new WaitForSeconds(Random.Range(1f, 3f)); //wählt in zufälligen Abständen neue Richtung //wählt zufällige Richtung
        character.rb.rotation = Random.Range(0f, 360f);
        chooseDir = false;
    }
}
