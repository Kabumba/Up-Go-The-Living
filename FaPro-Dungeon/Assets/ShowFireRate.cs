using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFireRate : MonoBehaviour
{
    void Update()
    {
        gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Fire rate: " + GameController.GetDelayBetweenShots().ToString("G1");
    }
}
