﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndFlag : MonoBehaviour
{
    [SerializeField]private TimeOnLevel timeOnLevelScript;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (SceneManager.GetActiveScene().name.Equals("StoryLevel"))
            {
                timeOnLevelScript.uploadLevelCompletionTime();
                switch (Grid.gameStateManager.currentLevel)
                {
                    case "LVL1":
                        Grid.gameStateManager.currentLevel = "LVL2";
                        break;

                    case "LVL2":
                        Grid.gameStateManager.currentLevel = "LVL3";
                        break;
                    case "LVL3":
                        Grid.gameStateManager.currentLevel = "LVL4";
                        break;
                    case "LVL4":
                        Grid.gameStateManager.currentLevel = "LVL5";
                        break;
                    case "LVL5":
                        Grid.gameStateManager.currentLevel = "LVL6";
                        break;
                    case "LVL6":
                        Grid.gameStateManager.currentLevel = "LVL7";
                        break;
                    case "LVL7":
                        Grid.gameStateManager.currentLevel = "LVL8";
                        break;
                    case "LVL8":
                        Grid.gameStateManager.currentLevel = "LVL9";
                        break;
                    case "LVL9":
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
