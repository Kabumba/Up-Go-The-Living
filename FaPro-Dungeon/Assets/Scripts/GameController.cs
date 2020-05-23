using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    private static int health = 6;

    private static int maxHealth = 6;

    private static int coins = 0;

    private static float damage = 3.5f;

    private static float moveSpeed = 4f;

    private static float maxMoveSpeed;

    private static float fireRate = 0.5f;

    private static float range = 23.75f;

    private static float bulletSpeed = 7f;

    private static float bulletSize = 0.5f;

    private static float invincibleAfterHit = 1f;

    private static float lasthit;

    private static bool invincible = false;

    public static int Health { get => health; set => health = value; }

    public static int MaxHealth { get => maxHealth; set => maxHealth = value; }

    public static int Coins { get => coins; set => coins = value; }

    public static float Damage { get => damage; set => damage = value; }

    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    public static float FireRate { get => fireRate; set => fireRate = value; }

    public static float Range { get => range; set => range = value; }

    public static float BulletSpeed { get => bulletSpeed; set => bulletSpeed = value; }

    public static float BulletSize { get => bulletSize; set => bulletSize = value; }

    public static float InvincibleAfterHit { get => invincibleAfterHit; set => invincibleAfterHit = value; }

    public static List<Item> items;

    public Text healthText;

    public Text coinText;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        items = new List<Item>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + health;
        coinText.text = "Coins: " + coins;
        UpdateItems();
        if(Time.time > lasthit + invincibleAfterHit)
        {
            invincible = false;
        }
    }

    public void UpdateItems()
    {
        foreach (Item item in items)
        {
            item.OnUpdate();
        }
    }

    public static void OnFireItems()
    {
        foreach (Item item in items)
        {
            item.OnFire();
        }
    }

    public static void DamagePlayer(int damage)
    {
        if (!invincible)
        {
            health = Mathf.Max(0, health - damage);
            foreach (Item item in items)
            {
                item.OnDamageTaken();
            }
            if (Health <= 0)
            {
                KillPlayer();
            }
            lasthit = Time.time;
            invincible = true;
        }
    }

    IEnumerator InvincibilityDelay()
    {
        invincible = true;
        yield return new WaitForSeconds(invincibleAfterHit);
        invincible = false;
    }

    public static void HealPlayer(int healAmount)
    {
        health = Mathf.Min(maxHealth, Health + healAmount);
    }

    public static void KillPlayer()
    {

    }

    public static void ChangeMaxHealth(int maxHealthChange)
    {
        maxHealth += maxHealthChange;
    }

    public static void ChangeCoins(int coinChange)
    {
        coins += coinChange;
    }

    public static void ChangeDamage(float damageChange)
    {
        damage += damageChange;
    }

    public static void ChangeMoveSpeed(float moveSpeedChange)
    {
        moveSpeed += moveSpeedChange;
    }

    public static void ChangeFireRate(float fireRateChange)
    {
        fireRate += fireRateChange;
    }

    public static void ChangeRange(float rangeChange)
    {
        range += rangeChange;
    }

    public static void ChangeBulletSpeed(float bulletSpeedChange)
    {
        bulletSpeed += bulletSpeedChange;
    }

    public static void ChangeBulletSize(float bulletSizeChange)
    {
        bulletSize += bulletSizeChange;
    }
}
