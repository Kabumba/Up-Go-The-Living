using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite leftFullHeart;
    public Sprite leftEmptyHeart;
    public Sprite rightFullHeart;
    public Sprite rightEmptyHeart;
    private void Update()
    {
        numOfHearts = GameController.MaxHealth;
        health = GameController.Health;
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health && (i % 2 == 0))
            {
                hearts[i].sprite = leftFullHeart;
            }
            else if (i < health && (i % 2 != 0))
            {
                hearts[i].sprite = rightFullHeart;
            }
            else if(i % 2 == 0)
            {
                hearts[i].sprite = leftEmptyHeart;
            }
            else
            {
                hearts[i].sprite = rightEmptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
