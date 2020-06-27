using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poisoned : EnemyEffect
{
    public int poisonTicks = 0;

    public float poisonDamage = 1f;

    float greenincrease;

    public override void OnApply()
    {
        poisonTicks = 3;
        if (gameObject.GetComponents<Poisoned>().Length > 1)
        {
            gameObject.GetComponents<Poisoned>()[0].poisonTicks = poisonTicks;
            Destroy(this);
        }
        else
        {
            SpriteRenderer spr = GetComponent<EnemyController>().GetSpriteRenderer();
            Color temp = spr.color;
            greenincrease = Mathf.Min(255 - temp.g, 100f);
            temp.g += greenincrease;
            spr.color = temp;
            StartCoroutine("PoisonTicks");
        }
    }

    public override void OnDeath()
    {
        Color temp = GetComponent<EnemyController>().GetSpriteRenderer().color;
        temp.g = Mathf.Max(0, temp.g - greenincrease);
        GetComponent<EnemyController>().GetSpriteRenderer().color = temp;
    }

    IEnumerator PoisonTicks()
    {
        while (poisonTicks > 0)
        {
            yield return new WaitForSeconds(1f);
            GetComponent<EnemyController>().DamageEnemy(poisonDamage);
            poisonTicks -= 1;
            Debug.Log("Tick");
        }
        OnDeath();
        Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
