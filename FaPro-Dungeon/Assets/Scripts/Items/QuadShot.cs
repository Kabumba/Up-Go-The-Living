using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuadShot : Item
{
    public override void OnPickup()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        ShooterController shc = player.GetComponent<ShooterController>();
        Quaternion rotationBefore = player.transform.rotation;
        player.transform.rotation = Quaternion.Euler(0, 0, 0);
        BulletShooter thirdBS = Instantiate(shc.bulletShooters[0], transform.position, transform.rotation);
        thirdBS.transform.parent = player.transform;
        thirdBS.name = "Main Middle Left";
        shc.bulletShooters.Add(thirdBS);
        BulletShooter fourthBS = Instantiate(shc.bulletShooters[0], transform.position, transform.rotation);
        fourthBS.transform.parent = player.transform;
        fourthBS.name = "Main Middle Right";
        shc.bulletShooters.Add(fourthBS);
        if (!GameController.Contains("TripleShot"))
        {
            GameController.ChangeFireDelay(new FireDelayUp());
        }
        else
        {
            foreach (BulletShooter bs in shc.bulletShooters.ToList<BulletShooter>())
            {
                if (bs.name.Equals("Main Middle"))
                {
                    shc.bulletShooters.Remove(bs);
                    Destroy(bs.gameObject);
                }
            }
            GameController.ChangeFireDelay(new FireDelayDown());
        }
        foreach (BulletShooter bs in shc.bulletShooters)
        {
            bs.resetLastFire();
            bs.fireShotOffset = 0;
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
                case ("Main Middle Left"):
                    bs.gameObject.transform.position = new Vector3(player.transform.position.x - 0.08f, player.transform.position.y + 0.433f, 0f);
                    bs.gameObject.transform.rotation = Quaternion.Euler(0, 0, 15);
                    break;
                case ("Main Middle Right"):
                    bs.gameObject.transform.position = new Vector3(player.transform.position.x + 0.08f, player.transform.position.y + 0.433f, 0f);
                    bs.gameObject.transform.rotation = Quaternion.Euler(0, 0, -15);
                    break;
                default:
                    break;
            }
        }
        shc.UpdateBulletEffects();
        player.transform.rotation = rotationBefore;
    }

    private class FireDelayUp : Statdecorator
    {
        public override float GetStat()
        {
            return (next.GetStat() * 2.1f) + 3;
        }
    }

    private class FireDelayDown : Statdecorator
    {
        public override float GetStat()
        {
            return next.GetStat()/3;
        }
    }
}
