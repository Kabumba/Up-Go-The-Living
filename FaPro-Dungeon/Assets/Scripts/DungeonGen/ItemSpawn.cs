using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    public Item[] items;
    public Transform itemPos;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            int e = UnityEngine.Random.Range(0, items.Length);
            Instantiate(items[e], itemPos.position, itemPos.rotation);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
