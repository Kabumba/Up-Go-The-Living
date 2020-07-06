using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject gameOverScreenUI;

    public GameObject player;

    private void Update()
    {
        if (GameController.dead)
        {
            player.SetActive(false);
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
