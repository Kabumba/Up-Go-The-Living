using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShot : Item
{
    public override void OnPickup()
    {
        if (!GameController.Contains("QuadShot"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            ShooterController shc = player.GetComponent<ShooterController>();
            Quaternion rotationBefore = player.transform.rotation;
            player.transform.rotation = Quaternion.Euler(0, 0, 0);
            BulletShooter thirdBS = Instantiate(shc.bulletShooters[0], transform.position, transform.rotation);
            thirdBS.transform.parent = player.transform;
            thirdBS.name = "Main Middle";
            shc.bulletShooters.Add(thirdBS);
            foreach (BulletShooter bs in shc.bulletShooters)
            {
                bs.resetLastFire();
                bs.fireShotOffset = 0;
                switch (bs.gameObject.name)
                {
                    case ("Main Left"):
                        bs.gameObject.transform.position = new Vector3(player.transform.position.x + -0.25f, player.transform.position.y + 0.433f, 0f);
                        bs.gameObject.transform.rotation = Quaternion.Euler(0, 0, 45);
                        bs.fireShotDelay = -1;
                        break;
                    case ("Main Right"):
                        bs.gameObject.transform.position = new Vector3(player.transform.position.x + 0.25f, player.transform.position.y + 0.433f, 0f);
                        bs.gameObject.transform.rotation = Quaternion.Euler(0, 0, -45);
                        bs.fireShotDelay = -1;
                        break;
                    case ("Main Middle"):
                        bs.gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.433f, 0f);
                        bs.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                        bs.fireShotDelay = -1;
                        break;
                    default:
                        bs.fireShotDelay = 1;
                        break;
                }
            }
            shc.UpdateBulletEffects();
            player.transform.rotation = rotationBefore;
            GameController.ChangeFireDelay(new FireDelayUp());
        }
    }

    private class FireDelayUp : Statdecorator
    {
        public override float GetStat()
        {
            return (next.GetStat() * 2.1f) + 3;
        }
    }
}
