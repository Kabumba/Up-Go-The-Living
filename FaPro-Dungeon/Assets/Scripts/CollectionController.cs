using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Item
{
    public string name;

    public string description;

    public Sprite itemImage;

}


public class CollectionController : MonoBehaviour
{

    public Item item;

    public int healthChange;

    public int maxHealthChange;

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
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameController.HealPlayer(healthChange);
            GameController.ChangeMaxHealth(maxHealthChange);
            GameController.ChangeDamage(damageChange);
            GameController.ChangeMoveSpeed(moveSpeedChange);
            GameController.ChangeFireRate(fireRateChange);
            GameController.ChangeRange(rangeChange);
            GameController.ChangeBulletSpeed(bulletSpeedChange);
            GameController.ChangeBulletSize(bulletSizeChange);
            Destroy(gameObject);
        }
    }
}
