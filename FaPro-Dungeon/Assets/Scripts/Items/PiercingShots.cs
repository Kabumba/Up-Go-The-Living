﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingShots : Item
{
    public Sprite pierceSprite;
    public override void OnPickup()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        ShooterController shc = player.GetComponent<ShooterController>();
        shc.AddBulletEffectToAll(new Piercing());
    }
}
