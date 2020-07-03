using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject gameOverScreenUI;

    private void Update()
    {
        if (GameController.dead)
        {
            GameObject.FindGameObjectWithTag("Player").SetActive(false);
            gameOverScreenUI.SetActive(true);
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
