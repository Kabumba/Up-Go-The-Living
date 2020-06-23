using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBarrage : Item
{
    public override void OnPickup()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        List<BulletShooter> bss = player.GetComponent<ShooterController>().bulletShooters;
        foreach(BulletShooter bs in bss)
        {
            bs.barrageCount = 3;
        }
        GameController.ChangeFireRate(GameController.FireRate);
    }
}
