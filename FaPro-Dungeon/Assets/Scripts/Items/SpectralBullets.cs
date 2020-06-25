using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpectralBullets : Item
{
    public override void OnPickup()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        ShooterController shc = player.GetComponent<ShooterController>();
        Spectral spectral = GameObject.Find("BulletEffects").AddComponent<Spectral>() as Spectral;
        shc.AddBulletEffectToAll(spectral);
    }
}