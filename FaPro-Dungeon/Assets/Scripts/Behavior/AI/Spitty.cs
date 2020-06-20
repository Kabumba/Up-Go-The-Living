using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitty : AI
{
    //public float obstacleStunTime;

    public bool shoot;

    public override void StateChanges()
    {
        if (shoot && character.shc != null && PlayerLinedUp())
        {
            SetState(new RangeAttack(character, Target.None));
        }
        else
        {
            SetState(new WanderCardinal(character, false));
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

}
