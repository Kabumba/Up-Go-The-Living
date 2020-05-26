using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public abstract class Item : MonoBehaviour
{
    public new string name;

    public string description;

    public Sprite itemImage;

    //Aus der Klasse erben um festzulegen was passieren soll wenn das Item aufgesammelt wird.
    public virtual void OnPickup()
    {
        //CollectionController.Destroy(gameObject);
    }

    //Aus der Klasse erben um festzulegen was das Item machen soll wenn man einen Schuss abfeuert.
    public virtual void OnFire()
    {

    }

    //Aus der Klasse erben um festzulegen was das Item bei jedem Frameupdate machen soll.
    public virtual void OnUpdate()
    {

    }

    //Aus der Klasse erben um festzulegen was das Item machen soll, wenn der Spieler Schaden kriegt.
    public virtual void OnDamageTaken()
    {

    }
}


public class CollectionController : MonoBehaviour
{

    public Item item;

    public int healthChange;

    public int maxHealthChange;

    public int coinChange;

    public float damageChange;

    public float moveSpeedChange;

    public float fireRateChange;

    public float rangeChange;

    public float bulletSpeedChange;

    public float bulletSizeChange;



    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = item.itemImage;
        /*Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
        GetComponent<PolygonCollider2D>().isTrigger = true;*/
    }

    //Statuswertänderungen wenn das Item eingesammelt wird.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            item.OnPickup();
            /* 
            GameController.HealPlayer(healthChange);
            GameController.ChangeMaxHealth(maxHealthChange);
            GameController.ChangeCoins(coinChange);
            GameController.ChangeDamage(damageChange);
            GameController.ChangeMoveSpeed(moveSpeedChange);
            GameController.ChangeFireRate(fireRateChange);
            GameController.ChangeRange(rangeChange);
            GameController.ChangeBulletSpeed(bulletSpeedChange);
            GameController.ChangeBulletSize(bulletSizeChange); 
            */
            Destroy(gameObject);
        }
    }
}


