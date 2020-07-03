using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    public GameObject victoryScreenUI;

    private void Update()
    {
        if (GameController.victory)
        {
            GameObject.FindGameObjectWithTag("Player").SetActive(false);
            victoryScreenUI.SetActive(true);
        }
    }

    public void Restart()
    {
        GameController.resetStats();
        SceneManager.LoadScene("Main");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
