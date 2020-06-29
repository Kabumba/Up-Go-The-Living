using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyEffect : MonoBehaviour
{
    public virtual void OnDeath() { }

    public virtual void OnApply() { }
}

public abstract class AIChange : EnemyEffect
{
    public AI previousAI;

    public AI newAI;

    public float duration;

    public IEnumerator ChangeAI<changeTo>() where changeTo : AI
    {
        foreach (AIChange aic in GetComponents<AIChange>())
        {
            if (aic.newAI != null && aic.newAI.enabled)
            {
                previousAI = aic.previousAI;
                aic.newAI.enabled = false;
                break;
            }
        }
        if (previousAI == null)
        {
            previousAI = GetComponent<AI>();
            previousAI.enabled = false;
        }

        newAI = gameObject.AddComponent<changeTo>();
        yield return new WaitForSeconds(duration);
        AI destroy = newAI;
        newAI = null;
        Destroy(destroy);

        print("destroyed");
        bool enablePreviousAI = true;
        foreach (AIChange aic in GetComponents<AIChange>())
        {
            if (aic.newAI != null && !aic.newAI.enabled)
            {

                print("other AI enabled");
                print(aic.newAI.GetType());
                aic.newAI.enabled = true;
                enablePreviousAI = false;
                break;
            }
        }
        if (enablePreviousAI)
        {
            print("previous AI enabled");
            previousAI.enabled = true;
        }

        Destroy(this);
    }

}

