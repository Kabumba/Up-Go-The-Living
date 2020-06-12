using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Prismabrille : Item
{
    public override void OnPickup()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        ShooterController shc = player.GetComponent<ShooterController>();
        Quaternion rotationBefore = player.transform.rotation;
        player.transform.rotation = Quaternion.Euler(0, 0, 0);
        foreach (BulletShooter bs in shc.bulletShooters)
        {
            switch (bs.gameObject.name)
            {
                case ("Main Left"):
                    bs.gameObject.transform.position = new Vector3(player.transform.position.x + -0.25f, player.transform.position.y + 0.433f, 0f);
                    bs.gameObject.transform.rotation = Quaternion.Euler(0, 0, 45);
                    break;
                case ("Main Right"):
                    bs.gameObject.transform.position = new Vector3(player.transform.position.x + 0.25f, player.transform.position.y + 0.433f, 0f);
                    bs.gameObject.transform.rotation = Quaternion.Euler(0, 0, -45);
                    break;
            }
        }
        player.transform.rotation = rotationBefore;
    }
}

