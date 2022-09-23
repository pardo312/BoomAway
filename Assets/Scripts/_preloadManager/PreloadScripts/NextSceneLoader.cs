using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BoomAway.Assets.Scripts.PreloadManager
{
    public class NextSceneLoader : MonoBehaviour
    {
        private void Awake()
        {
#if UNITY_EDITOR
            Grid.nextSceneLoader.SetPreload();
#endif
#if !UNITY_EDITOR
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
#endif
        }
        private void SetPreload()
        {
            SceneManager.LoadScene(1);
        }
    }
}