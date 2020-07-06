using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDamage : MonoBehaviour
{
    void Update()
    {
        float damage = Mathf.RoundToInt(GameController.GetEffectiveDamage() * 10f) / 10f;
        gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Schaden: " + damage.ToString();
    }
}
