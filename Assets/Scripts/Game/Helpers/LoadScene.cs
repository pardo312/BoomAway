using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void loadScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
    public void loadSceneStory(string currentStoryLevel){
        Grid.gameStateManager.currentLevel = currentStoryLevel;
        SceneManager.LoadScene("StoryLevel");
    }
    public void ApplicationQuit(){
        Application.Quit();
    }
}
