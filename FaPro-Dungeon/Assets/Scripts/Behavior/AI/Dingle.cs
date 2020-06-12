using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dingle : AI
{
    private int pattern = 1;
    private float lastPatternChange;
    private int dashCount;
    public int numberOfDashes;
    public float dashSpeed;
    public float dashTime;
    public float idleTime;
    private float lastDashTime;

    public override void StateChanges()
    {
        switch (pattern)
        {
            case 1:
                if (Time.time >= lastPatternChange + 5)
                {
                    lastPatternChange = Time.time;
                    pattern = 2;
                }
                else
                {
                    character.mvc.RotateTowards(character.player);
                    SetState(new RangeAttack(character));
                }
                break;
            case 2:
                if (dashCount >= numberOfDashes)
                {
                    dashCount = 0;
                    lastPatternChange = Time.time;
                    pattern = 3;
                }
                else
                {
                    if (Time.time >= lastDashTime + dashTime + idleTime)
                    {
                        lastDashTime = Time.time;
                        SetState(new Dash(character, Target.Player, dashSpeed, dashTime));
                        dashCount++;
                    }
                    else
                    {
                        SetState(new Idle(character));
                    }
                }
                break;
            case 3:
                if (Time.time >= lastPatternChange + 5)
                {
                    lastPatternChange = Time.time;
                    pattern = 1;
                }
                else
                {
                    SetState(new Idle(character));
                }
                break;
            default:
                pattern = 1;
                break;
        }
    }

    IEnumerator Wait(float s)
    {
        yield return new WaitForSeconds(s);
    }
    
}
