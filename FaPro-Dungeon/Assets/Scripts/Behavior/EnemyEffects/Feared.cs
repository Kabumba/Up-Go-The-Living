using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feared : EnemyEffect
{
    System.Type previousAI;

    public override void OnApply()
    {
        SpriteRenderer spr = GetComponent<EnemyController>().GetSpriteRenderer();
        StartCoroutine(Fear());
    }

    IEnumerator Fear()
    {
        previousAI = GetComponent<AI>().GetType();
        Destroy(GetComponent<AI>());

        gameObject.AddComponent<FleeAI>();
        yield return new WaitForSeconds(4);
        Destroy(GetComponent<AI>());

        gameObject.AddComponent(previousAI);

        Destroy(this);
    }
}
