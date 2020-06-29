using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Petrified : AIChange
{
    public override void OnApply()
    {
        duration = 4;
        SpriteRenderer spr = GetComponent<EnemyController>().GetSpriteRenderer();
        StartCoroutine(ChangeAI<IdleAI>());
    }
}
