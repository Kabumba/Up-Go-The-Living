using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackheadShot : Item
{
    public override void OnPickup()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        ShooterController shc = player.GetComponent<ShooterController>();
        Quaternion rotationBefore = player.transform.rotation;
        player.transform.rotation = Quaternion.Euler(0, 0, 0);
        BulletShooter backBS = Instantiate(shc.bulletShooters[0], transform.position, transform.rotation);
        backBS.transform.parent = player.transform;
        backBS.name = "Main Back";
        shc.bulletShooters.Add(backBS);
        backBS.gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 0.433f, 0f);
        backBS.gameObject.transform.rotation = Quaternion.Euler(0, 0, 180);
        backBS.fireShotDelay = 2;
        player.transform.rotation = rotationBefore;
    }
}
