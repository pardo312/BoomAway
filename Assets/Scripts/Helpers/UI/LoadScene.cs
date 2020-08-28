using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void loadScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
    public void ApplicationQuit(){
        Application.Quit();
    }
}
