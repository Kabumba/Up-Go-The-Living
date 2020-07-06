using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemText : MonoBehaviour
{
    public bool itemPickedUp = false;

    public GameObject itemName;

    public GameObject itemDescription;

    public Item item;

    private void Update()
    {
        if (itemPickedUp)
        {
            ShowItemText();
        }
    }

    private void ShowItemText()
    {
        itemName.GetComponent<TMPro.TextMeshProUGUI>().text = item.name;
        itemDescription.GetComponent<TMPro.TextMeshProUGUI>().text = item.description;

        itemName.GetComponent<TMPro.TextMeshProUGUI>().enabled = true;
        itemDescription.GetComponent<TMPro.TextMeshProUGUI>().enabled = true;

        StartCoroutine(DelayRoutine());
    }

    IEnumerator DelayRoutine()
    {
        yield return new WaitForSeconds(5f);
        itemPickedUp = false;
        itemName.GetComponent<TMPro.TextMeshProUGUI>().enabled = false;
        itemDescription.GetComponent<TMPro.TextMeshProUGUI>().enabled = false;
    }
}
