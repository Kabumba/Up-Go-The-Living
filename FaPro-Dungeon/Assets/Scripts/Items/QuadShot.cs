using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuadShot : Item
{
    public override void OnPickup()
    {
        if (!GameController.Contains("TripleShot"))
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
            foreach (BulletShooter bs in shc.bulletShooters)
            {
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
                    case ("Main Middle Left"):
                        bs.gameObject.transform.position = new Vector3(player.transform.position.x - 0.08f, player.transform.position.y + 0.433f, 0f);
                        bs.gameObject.transform.rotation = Quaternion.Euler(0, 0, 15);
                        bs.fireShotDelay = -1;
                        break;
                    case ("Main Middle Right"):
                        bs.gameObject.transform.position = new Vector3(player.transform.position.x + 0.08f, player.transform.position.y + 0.433f, 0f);
                        bs.gameObject.transform.rotation = Quaternion.Euler(0, 0, -15);
                        bs.fireShotDelay = -1;
                        break;
                    default:
                        bs.fireShotDelay = -1;
                        break;
                }
            }
            player.transform.rotation = rotationBefore;
            GameController.ChangeFireRate(GameController.FireRate * 3);
        }
        else
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            ShooterController shc = player.GetComponent<ShooterController>();
            Quaternion rotationBefore = player.transform.rotation;
            player.transform.rotation = Quaternion.Euler(0, 0, 0);
            foreach(BulletShooter bs in shc.bulletShooters.ToList<BulletShooter>())
            {
                if(bs.name.Equals("Main Middle"))
                {
                    shc.bulletShooters.Remove(bs);
                    Destroy(bs.gameObject);
                }
            }
            BulletShooter thirdBS = Instantiate(shc.bulletShooters[0], transform.position, transform.rotation);
            thirdBS.transform.parent = player.transform;
            thirdBS.name = "Main Middle Left";
            shc.bulletShooters.Add(thirdBS);
            BulletShooter fourthBS = Instantiate(shc.bulletShooters[0], transform.position, transform.rotation);
            fourthBS.transform.parent = player.transform;
            fourthBS.name = "Main Middle Right";
            shc.bulletShooters.Add(fourthBS);
            foreach (BulletShooter bs in shc.bulletShooters)
            {
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
                    case ("Main Middle Left"):
                        bs.gameObject.transform.position = new Vector3(player.transform.position.x - 0.08f, player.transform.position.y + 0.433f, 0f);
                        bs.gameObject.transform.rotation = Quaternion.Euler(0, 0, 15);
                        bs.fireShotDelay = -1;
                        break;
                    case ("Main Middle Right"):
                        bs.gameObject.transform.position = new Vector3(player.transform.position.x + 0.08f, player.transform.position.y + 0.433f, 0f);
                        bs.gameObject.transform.rotation = Quaternion.Euler(0, 0, -15);
                        bs.fireShotDelay = -1;
                        break;
                    default:
                        bs.fireShotDelay = -1;
                        break;
                }
            }
            player.transform.rotation = rotationBefore;
            GameController.ChangeFireRate(GameController.FireRate/3);
        }
    }
}
