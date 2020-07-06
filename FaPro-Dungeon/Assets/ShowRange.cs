using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRange : MonoBehaviour
{
    void Update()
    {
        float range = Mathf.RoundToInt(GameController.GetRangeStat());
        gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Reichweite: " + range.ToString();
    }
}
