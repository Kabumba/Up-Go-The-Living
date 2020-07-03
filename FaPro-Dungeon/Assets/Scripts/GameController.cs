using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Statdecorator
{
    public Statdecorator next;

    public virtual float GetStat()
    {
        return next.GetStat();
    }
}



public class GameController : MonoBehaviour
{
    //------------ATTRIBUTES---------------------------------------------------
    

    //------------Health-------------------------------------

    //Default: 6
    private static int health = 6;

    //Default: 6
    private static int maxHealth = 6;


    //------------Coins--------------------------------------

    //Default: 0
    private static int coins = 0;


    //------------Damage-------------------------------------

    //Default: 3.5
    private static float baseDamage = 3.5f;

    //Default: 0
    private static float damageUp = 0;

    //Default: 1
    private static float damageMultiplier = 1f;

    //Default: 0
    public static float damageThroughRage = 0f;


    //------------MoveSpeed----------------------------------

    //Default: 1
    private static float moveSpeed = 1f;

    //Default: 4
    private static float baseMoveSpeedFactor = 4f;

    //Default: 2
    private static float maxMoveSpeed = 2f;


    //------------FireRate-----------------------------------

    //Default: 0
    private static float baseFireRate = 0;

    private static Statdecorator fireRateDecor;

    private static Statdecorator fireDelayDecor;


    //------------Range--------------------------------------

    //Default: 23.75
    private static float range = 23.75f;

    //Default: 30f
    private static float baseRangeDivider = 30f;


    //------------BulletSpeed--------------------------------

    //Default: 1
    private static float bulletSpeed = 1f;

    //Default: 7
    private static float baseBulletSpeed = 7f;


    //------------BulletSize---------------------------------

    //Default: 0.5
    private static float bulletSize = 0.5f;


    //------------Invincibility------------------------------

    //Default: 1
    private static float invincibleAfterHit = 1f;

    private static float lasthit;

    private static bool invincible = false;



    public static List<Item> items;

    public static int floorNumber = 1;


    //------------METHODS------------------------------------------------------


    //------------General------------------------------------

    private void Start()
    {
        fireRateDecor = new BaseFireRate();
        fireDelayDecor = new BaseFireDelay();
    }

    void Awake()
    {
        items = new List<Item>();
    }

    void Update()
    {
        UpdateItems();
        if (Time.time > lasthit + invincibleAfterHit)
        {
            invincible = false;
        }
    }

    public static void resetStats()
    {
        health = 6;
        maxHealth = 6;
        coins = 0;
        baseDamage = 3.5f;
        damageUp = 0;
        damageMultiplier = 1f;
        damageThroughRage = 0f;
        moveSpeed = 1f;
        baseMoveSpeedFactor = 4f;
        maxMoveSpeed = 2f;
        baseFireRate = 0;
        range = 23.75f;
        baseRangeDivider = 30f;
        bulletSpeed = 1f;
        baseBulletSpeed = 7f;
        bulletSize = 0.5f;
        invincibleAfterHit = 1f;
        invincible = false;
        floorNumber = 1;
        fireRateDecor = new BaseFireRate();
        fireDelayDecor = new BaseFireDelay();
        items = new List<Item>();
    }

    private void UpdateItems()
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

    public static bool Contains(string itemName)
    {
        foreach (Item item in items)
        {
            if (item.name.Equals(itemName))
            {
                return true;
            }
        }
        return false;
    }


    //------------Health-------------------------------------

    public static int GetHealth()
    {
        return health;
    }

    public static int GetMaxHealth()
    {
        return maxHealth;
    }

    public static void HealPlayer(int healAmount)
    {
        health = Mathf.Min(maxHealth, health + healAmount);
    }

    public static void AddMaxHealth(int maxHealthChange)
    {
        maxHealth += maxHealthChange;
    }


    //------------Coins--------------------------------------

    public static void AddCoins(int coinChange)
    {
        coins += coinChange;
    }


    //------------Damage-------------------------------------

    public static float GetEffectiveDamage()
    {
        return baseDamage * Mathf.Sqrt(damageUp * 1.2f + 1) * damageMultiplier;
    }

    public static void AddDamage(float damageChange)
    {
        damageUp += damageChange;
    }

    public static void MultiplyDamageMultiplier(float multiplier)
    {
        damageMultiplier *= multiplier;
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
            if (health <= 0)
            {
                KillPlayer();
            }
            lasthit = Time.time;
            invincible = true;
        }
    }

    public static void KillPlayer()
    {
        GameObject.FindGameObjectWithTag("Player").SetActive(false);
        FindObjectOfType<GameOverScreen>().gameOverScreenUI.SetActive(true);
    }


    //------------MoveSpeed----------------------------------

    public static float GetMoveSpeed()
    {
        return moveSpeed * baseMoveSpeedFactor;
    }

    public static float GetMoveSpeedStat()
    {
        return moveSpeed;
    }

    public static void AddMoveSpeed(float moveSpeedChange)
    {
        moveSpeed = Mathf.Max(0.1f, Mathf.Min(moveSpeedChange + moveSpeed, maxMoveSpeed));
    }


    //------------FireRate-----------------------------------

    public static float GetDelayBetweenShots()
    {
        return (GetFireDelay() + 1) / 30;
    }

    public static float GetShotsPerSecond()
    {
        return 1/GetDelayBetweenShots();
    }

    public static void ChangeFireRate(Statdecorator change)
    {
        change.next = fireRateDecor;
        fireRateDecor = change;
    }

    public static void ChangeFireDelay(Statdecorator change)
    {
        change.next = fireDelayDecor;
        fireDelayDecor = change;
    }

    private static float GetBaseFireDelay()
    {
        float firerate = GetFireRate();
        if (firerate >= 0)
        {
            return 16 - 6 * Mathf.Sqrt(firerate * 1.3f * 1);
        }
        else
        {
            if (firerate > -0.77f)
            {
                return 16 - 6 * Mathf.Sqrt(firerate * 1.3f * 1) - 6 * firerate;
            }
            else
            {
                return 16 - 6 * firerate;
            }
        }
    }

    private static float GetFireRate()
    {
        return fireRateDecor.GetStat();
    }

    private static float GetFireDelay()
    {
        return fireDelayDecor.GetStat();
    }

    private class BaseFireRate : Statdecorator
    {
        public override float GetStat()
        {
            return baseFireRate;
        }
    }

    private class BaseFireDelay : Statdecorator
    {
        public override float GetStat()
        {
            return GetBaseFireDelay();
        }
    }


    //------------Range--------------------------------------

    public static float GetBulletLifeTime()
    {
        return range / baseRangeDivider;
    }

    public static float GetRangeStat()
    {
        return range;
    }

    public static void AddRange(float rangeChange)
    {
        range += rangeChange;
    }


    //------------BulletSpeed--------------------------------

    public static float GetBulletSpeed()
    {
        return bulletSpeed * baseBulletSpeed;
    }

    public static float GetBulletSpeedStat()
    {
        return bulletSpeed;
    }

    public static void AddBulletSpeed(float bulletSpeedChange)
    {
        bulletSpeed = Mathf.Max(0.6f, bulletSpeedChange + bulletSpeed);
    }


    //------------BulletSize---------------------------------

    public static float GetBulletSize()
    {
        return bulletSize;
    }

    public static void AddBulletSize(float bulletSizeChange)
    {
        bulletSize += bulletSizeChange;
    }


    //------------Invincibility------------------------------

    IEnumerator InvincibilityDelay()
    {
        invincible = true;
        yield return new WaitForSeconds(invincibleAfterHit);
        invincible = false;
    }

}
