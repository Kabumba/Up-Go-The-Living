using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float range;

    public float damage;

    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        damage = GameController.Damage;
        range = GameController.Range;
        StartCoroutine(DeathDelay());
        transform.localScale = new Vector2(GameController.BulletSize, GameController.BulletSize);
        startPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Sorgt dafür, dass das Projektil zerstört wird, wenn es sich zu weit von seiner startpostion entfernt hat
    IEnumerator DeathDelay()
    {
        yield return new WaitUntil(() => Vector3.Distance(transform.position, startPosition) >= range);
        Destroy(gameObject);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if ("Enemy".Equals(collision.tag))
        {
            collision.gameObject.GetComponent<EnemyController>().Death();
            Destroy(gameObject);
        }
    }
}
