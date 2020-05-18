using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float range;

    public float damage;

    public float lifeTime;

    public bool isEnemyBullet = false;

    private Vector3 startPosition;

    private Vector2 lastPosition;

    private Vector2 currentPosition;

    private Vector2 playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        damage = GameController.Damage;
        range = GameController.Range;
        lifeTime = range / 30f;
        StartCoroutine(DeathDelay());
        if (!isEnemyBullet)
        {
            transform.localScale = new Vector2(GameController.BulletSize, GameController.BulletSize);
        }
        
        startPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnemyBullet)
        {
            currentPosition = transform.position;
            transform.position = Vector2.MoveTowards(transform.position, playerPosition, 5f * Time.deltaTime);
            if (currentPosition == lastPosition)
            {
                Destroy(gameObject);
            }
            lastPosition = currentPosition;
        }
    }

    public void GetPlayer(Transform player)
    {
        playerPosition = player.position;
    }

    //Sorgt dafür, dass das Projektil zerstört wird, wenn es sich zu weit von seiner startpostion entfernt hat
    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if ("Enemy".Equals(collision.tag) && !isEnemyBullet)
        {
            collision.gameObject.GetComponent<EnemyController>().Death();
            Destroy(gameObject);
        }

        if ("Player".Equals(collision.tag) && isEnemyBullet)
        {
            GameController.DamagePlayer(1);
            Destroy(gameObject);
        }
    }
}
