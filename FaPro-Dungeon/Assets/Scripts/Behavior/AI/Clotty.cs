using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clotty : AI
{
    public override void StateChanges()
    {
        if (character.IsPlayerInRange())
        {
            if (Vector3.Distance(transform.position, character.player.transform.position) <= character.attackRange)
            {
                SetState(new Wander(character, true));
            }
        }
        else
        {
            SetState(new Wander(character, false));
        }
    }

    private void Start()
    {
        //gameObject.transform.rotation = Quaternion.Euler(0, 0, -45);
    }
}
