using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRange : MonoBehaviour
{
    void Update()
    {
        gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Range: " + GameController.GetBulletLifeTime().ToString("G1");
    }
}
