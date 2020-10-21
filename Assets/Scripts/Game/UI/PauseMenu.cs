using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;
using System.Collections.Generic;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject deathMenuUI;
    [SerializeField] private GameObject tutorialUI;
    [SerializeField] private RetriesPerLevel retriesPer;

    private void Start()
    {
        retriesPer = this.GetComponent<RetriesPerLevel>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Grid.gameStateManager.canPause())
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                managePause();
            }
        }
        if(Grid.gameStateManager.health<0.1f){
            Die();
        }

    }
    public void managePause()
    {
        if (Grid.gameStateManager.IsPaused)
        {
            Resume();

        }
        else
        {
            Pause();
        }
    }
    private void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        StartCoroutine(unPause());
    }
    IEnumerator unPause()
    {
        yield return new WaitForSeconds(0.1f);
        Grid.gameStateManager.IsPaused = false;
    }
    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Grid.gameStateManager.IsPaused = true;
    }
    public void restart()
    {
        retriesPer.UploadRetriesMethod();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Grid.gameStateManager.initRestart();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void tutorial()
    {
        tutorialUI.SetActive(true);
    }

    //Tener un modo de de creador de mundos y uno de juegar online
    //Worldbuilder
    private void Die()
    {
        deathMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Grid.gameStateManager.IsPaused = true;
        Grid.gameStateManager.currentDeaths++;
    }
    IEnumerator changeEditing()
    {
        yield return new WaitForSeconds(0.2f);
        Grid.gameStateManager.editing = false;
    }
    public void goToMainMenu()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Grid.gameStateManager.initVariables();
        SceneManager.LoadScene("TitleScreen");
    }

}
