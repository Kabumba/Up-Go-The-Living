using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMovementSpeed : MonoBehaviour
{
    void Update()
    {
        float moveSpeed = Mathf.RoundToInt(GameController.GetMoveSpeedStat() * 10f) / 10f;
        gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Geschwindigkeit: " + moveSpeed.ToString();
    }
}
