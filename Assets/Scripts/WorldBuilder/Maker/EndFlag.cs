using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndFlag : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.CompareTag("Player"))
        {
            if (SceneManager.GetActiveScene().Equals("StoryLevel"))
            {
                Grid.gameStateManager.currentLevel = "LVL2"; 
                SceneManager.LoadScene("StoryLevel");
            }
        }
    }
}
