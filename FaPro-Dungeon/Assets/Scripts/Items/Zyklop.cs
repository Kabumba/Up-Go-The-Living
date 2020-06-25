using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zyklop : Item
{
    public override void OnPickup()
    {
        GameController.AddDamage(4);
        GameController.MultiplyDamageMultiplier(2);
        GameController.ChangeFireDelay(new FireDelayUp());


        GameObject player = GameObject.FindGameObjectWithTag("Player");
        ShooterController shc = player.GetComponent<ShooterController>();
        Quaternion rotationBefore = player.transform.rotation;
        player.transform.rotation = Quaternion.Euler(0, 0, 0);


        BulletShooter newMain = Instantiate(shc.bulletShooters[0], transform.position, transform.rotation);
        newMain.transform.parent = player.transform;
        newMain.name = "Main Middle";
        newMain.fireShotDelay = 0;

        newMain.bulletEffects = shc.bulletShooters[0].bulletEffects;
        newMain.bulletEffects.Add(new Polyphemus());

        newMain.gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, 0f);
        newMain.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        List<BulletShooter> toRemove = new List<BulletShooter>();
        foreach (BulletShooter bs in shc.bulletShooters)
        {
            if (bs.name.StartsWith("Main"))
            {
                toRemove.Add(bs);
            }
        }
        foreach (BulletShooter bs in toRemove)
        {
            shc.bulletShooters.Remove(bs);
            Destroy(bs.gameObject);
        }
        shc.bulletShooters.Add(newMain);
        player.transform.rotation = rotationBefore;
    }

    private class FireDelayUp : Statdecorator
    {
        public override float GetStat()
        {
            return (next.GetStat() * 2.1f) + 3;
        }
    }
}
