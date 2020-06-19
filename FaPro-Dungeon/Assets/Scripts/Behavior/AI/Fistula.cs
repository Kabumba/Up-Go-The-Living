using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fistula : AI
{
    public override void StateChanges()
    {
        SetState(new Forward(character));
    }

    private void Start()
    {
        //gameObject.transform.rotation = Quaternion.Euler(0, 0, -45);
    }
}
