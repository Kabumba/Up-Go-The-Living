using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampupDamage : BulletEffect
{
    
    private float lastIncrease;

    public override void OnInstantiate()
    {
        Color temp = GetComponent<SpriteRenderer>().color;
        Color neu = Color.white;
        neu.a = temp.a;
        GetComponent<SpriteRenderer>().color = neu;
    }

    public float RampDamage()
    {
        return gameObject.GetComponent<BulletController>().distanceTravelled * 0.5f;
    }

    public override void Tick()
    {
        gameObject.GetComponent<BulletController>().damage +=  RampDamage() - lastIncrease;
        lastIncrease = RampDamage();
        float scale = GetComponent<BulletController>().damage / GameController.GetEffectiveDamage();
        transform.localScale = new Vector2(GameController.GetBulletSize() * scale, GameController.GetBulletSize() * scale);
    }
}
