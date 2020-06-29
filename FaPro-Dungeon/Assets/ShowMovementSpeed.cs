using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMovementSpeed : MonoBehaviour
{
    void Update()
    {
        gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Movement speed: " + GameController.GetMoveSpeed().ToString("G");
    }
}
