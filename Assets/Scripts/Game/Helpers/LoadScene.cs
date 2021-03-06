﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadScene : MonoBehaviour
{

    private Animator animator;
    void Awake()
    {
        animator = GameObject.Find("FadeEffect").GetComponent<Animator>();
    }

    IEnumerator fadeIn(string sceneName){
        animator.SetBool("FadeIn",true);
        animator.SetBool("FadeOut",false);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(sceneName);
    }
    public void loadScene(string sceneName){
        StartCoroutine(fadeIn(sceneName));
    }
    
    public void loadSceneStory(string currentStoryLevel){
        Grid.gameStateManager.IsOnStoryMode = true;
        Grid.gameStateManager.currentLevel = currentStoryLevel;
        StartCoroutine(fadeIn("StoryLevel"));
    }
    public void ApplicationQuit(){
        #if (UNITY_EDITOR || DEVELOPMENT_BUILD)
            Debug.Log(this.name+" : "+this.GetType()+" : "+System.Reflection.MethodBase.GetCurrentMethod().Name); 
        #endif
        #if (UNITY_EDITOR)
            UnityEditor.EditorApplication.isPlaying = false;
        #elif (UNITY_STANDALONE) 
            Application.Quit();
        #elif (UNITY_WEBGL)
            Application.OpenURL("about:blank");
        #endif
    }
}
