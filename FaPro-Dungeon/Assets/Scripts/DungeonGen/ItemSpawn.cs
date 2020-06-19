using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    public List<Item> items;
    public Transform itemPos;

    public void Update()
    {
        items.RemoveAll(item => GameController.items.Contains(item));
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            int e = UnityEngine.Random.Range(0, items.Count);
            Instantiate(items[e], itemPos.position, itemPos.rotation);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
