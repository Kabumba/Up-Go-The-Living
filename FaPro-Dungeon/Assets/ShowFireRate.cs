using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFireRate : MonoBehaviour
{
    void Update()
    {
        float firerate = Mathf.RoundToInt(GameController.GetShotsPerSecond() * 10f)/10f;
        gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Feuerrate: " + firerate.ToString();
    }
}
