using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBulletSpeed : MonoBehaviour
{
    void Update()
    {
        gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Bullet speed: " + GameController.GetBulletSpeed().ToString("G1");
    }
}
