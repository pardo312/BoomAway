using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BoomAway.Assets.Scripts.PreloadManager
{
    public class NextSceneLoader : MonoBehaviour
    {
        void Start()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}