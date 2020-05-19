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

    private static float moveSpeed = 4;

    private static float fireRate = 0.5f;

    private static float range = 23.75f;

    private static float bulletSpeed = 7f;

    private static float bulletSize = 0.5f;

    public static int Health { get => health; set => health = value; }

    public static int MaxHealth { get => maxHealth; set => maxHealth = value; }

    public static int Coins { get => coins; set => coins = value; }

    public static float Damage { get => damage; set => damage = value; }

    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    public static float FireRate { get => fireRate; set => fireRate = value; }

    public static float Range { get => range; set => range = value; }

    public static float BulletSpeed { get => bulletSpeed; set => bulletSpeed = value; }

    public static float BulletSize { get => bulletSize; set => bulletSize = value; }

    public Text healthText;

    public Text coinText;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + health;
        coinText.text = "Coins: " + coins;
    }

    public static void DamagePlayer(int damage)
    {
        health = Mathf.Max(0, health-damage);
        if(Health <= 0)
        {
            KillPlayer();
        }
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
