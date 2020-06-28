using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Petrified : EnemyEffect
{
    System.Type previousAI;
    
    public override void OnApply()
    {
        SpriteRenderer spr = GetComponent<EnemyController>().GetSpriteRenderer();
        StartCoroutine(Petrify());
    }

    IEnumerator Petrify()
    {
        previousAI = GetComponent<AI>().GetType();
        Destroy(GetComponent<AI>());

        gameObject.AddComponent<IdleAI>();
        yield return new WaitForSeconds(4);
        Destroy(GetComponent<AI>());

        gameObject.AddComponent(previousAI);
        
        Destroy(this);
    }
}
