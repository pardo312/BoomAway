using UnityEngine;
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
        Grid.gameStateManager.currentLevel = currentStoryLevel;
        StartCoroutine(fadeIn("StoryLevel"));
    }
    public void ApplicationQuit(){
        Application.Quit();
    }
}
