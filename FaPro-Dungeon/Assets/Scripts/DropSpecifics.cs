using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpecifics : MonoBehaviour
{
    public List<Item> items;

    private void Update()
    {
        items.RemoveAll(item => GameController.items.Contains(item));
    }
}
