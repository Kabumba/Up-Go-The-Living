using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartOfRoom : AIChange
{
    public override void OnApply()
    {
        duration = 1;
        StartCoroutine(ChangeAI<IdleAI>());
    }
}
