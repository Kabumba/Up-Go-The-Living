using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : AI
{
    public float chargeSpeed;

    //public float obstacleStunTime;

    public bool shoot;

    public override void StateChanges()
    {
        if ("Charge".Equals(currentState.name))
        {

        }
        else
        {
            if (PlayerLinedUp())
            {
                SetState(new Charge(character, chargeSpeed));
            }
            else
            {
                SetState(new WanderCardinal(character, shoot));
            }
        }
    }

    private bool PlayerLinedUp()
    {
        Vector2 ppos = character.player.transform.position;
        Vector2 epos = transform.position;
        Vector2 diff = ppos - epos;
        if (Mathf.Abs(diff.x) < 0.1 && diff.y > 0)
        {
            character.mvc.SetRotation(new Vector2(0, 1));
            return true;
        }
        if (Mathf.Abs(diff.x) < 0.1 && diff.y <= 0)
        {
            character.mvc.SetRotation(new Vector2(0, -1));
            return true;
        }
        if (Mathf.Abs(diff.y) < 0.1 && diff.x > 0)
        {
            character.mvc.SetRotation(new Vector2(1, 0));
            return true;
        }
        if (Mathf.Abs(diff.y) < 0.1 && diff.x <= 0)
        {
            character.mvc.SetRotation(new Vector2(-1, 0));
            return true;
        }
        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Untagged"))
        {
            SetState(new WanderCardinal(character, shoot));
        }
    }
}
