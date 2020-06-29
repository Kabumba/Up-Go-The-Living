using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feared : AIChange
{
    public override void OnApply()
    {
        duration = 4;
        SpriteRenderer spr = GetComponent<EnemyController>().GetSpriteRenderer();
        StartCoroutine(ChangeAI<FleeAI>());
    }
}
