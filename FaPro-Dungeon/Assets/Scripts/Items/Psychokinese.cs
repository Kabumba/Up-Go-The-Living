﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Psychokinese : Item
{
    public override void OnPickup()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        ShooterController shc = player.GetComponent<ShooterController>();
        Homing homing = GameObject.Find("BulletEffects").AddComponent<Homing>() as Homing;
        shc.AddBulletEffectToAll(homing);
    }
}
