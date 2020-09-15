using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndFlag : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (SceneManager.GetActiveScene().name.Equals("StoryLevel"))
            {
                switch (Grid.gameStateManager.currentLevel)
                {
                    case "LVL1":
                        Grid.gameStateManager.currentLevel = "LVL2";
                        break;

                    case "LVL2":
                        Grid.gameStateManager.currentLevel = "LVL3";
                        break;
                    case "LVL3":
                        Grid.gameStateManager.currentLevel = "LVL1";
                        break;
                    default:
                        break;
                }
                SceneManager.LoadScene("StoryLevel");
            }
        }
    }
}
