using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : AI
{
    public float chargeSpeed;

    public float dashTime;

    private float lastDashtime;

    public override void StateChanges()
    {
        if ("Charge".Equals(currentState.name))
        {

        }
        else
        {
            if (Vector3.Distance(transform.position, character.player.transform.position) <= character.attackRange)
            {
                character.mvc.RotateTowards(character.player);
                SetState(new Charge(character, chargeSpeed));

            }
            else
            {
                SetState(new Follow(character, character.player));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Untagged"))
        {
            SetState(new Follow(character, character.player));
        }
    }
}
