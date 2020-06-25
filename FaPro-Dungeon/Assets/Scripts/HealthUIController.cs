using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIController : MonoBehaviour
{
    public GameObject heartContainer;

    private float fillValue;

    // Update is called once per frame
    void Update()
    {
        fillValue = (float)GameController.GetHealth();
        fillValue = fillValue / GameController.GetMaxHealth();
        heartContainer.GetComponent<Image>().fillAmount = fillValue;
    }
}
