using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDamage : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Damage: " + GameController.GetEffectiveDamage().ToString("G");
    }
}
