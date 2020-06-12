using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Target
{
    Player,
    None,
}

public class Dasher : AI
{
    public float dashSpeed;
    public float dashTime;
    public float idleTime;
    private float randomrange = 0.2f;
    private float lastDashTime;
    private float randomIdleTime;
    public Target target;

    private void Start()
    {
        lastDashTime = Time.time - dashTime;
        randomIdleTime = Random.Range(idleTime - randomrange * idleTime, idleTime + randomrange * idleTime);
    }

    public override void StateChanges()
    {
        if (Time.time >= lastDashTime + dashTime + randomIdleTime)
        {
            lastDashTime = Time.time;
            SetState(new Dash(character, target, dashSpeed, dashTime));
            randomIdleTime = Random.Range(idleTime - randomrange * idleTime, idleTime + randomrange * idleTime);
        }
        else
        {
            SetState(new Idle(character));
        }
    }
}
