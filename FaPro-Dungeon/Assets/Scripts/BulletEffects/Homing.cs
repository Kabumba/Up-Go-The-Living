using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing : BulletEffect
{
    private GameObject target;

    public float homeRange = 3;

    public override void Tick()
    {
        if (target!=null)
        {
            BulletController bulletController = GetComponent<BulletController>();
            bulletController.mvc.RotateTowards(target);
            bulletController.mvc.MoveForward();
        }
        else
        {
            float minDistance = float.MaxValue;
            GameObject closestEnemy = null;
            foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestEnemy = enemy;
                }
            }
            if(minDistance <= homeRange)
            {
                target = closestEnemy;
            }
        }
    }

    public override void OnInstantiate()
    {
        Color temp = GetComponent<SpriteRenderer>().color;
        Color neu = Color.magenta;
        neu.a = temp.a;
        GetComponent<SpriteRenderer>().color = neu;
    }
}
