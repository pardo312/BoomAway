using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BoomAway.Assets.Scripts.PreloadManager
{
    public class NextSceneLoader : MonoBehaviour
    {
        #if UNITY_EDITOR
            private void Awake()
            {
                Grid.nextSceneLoader.SetPreload();
            }
            private void SetPreload()
            {
                //nothingToDo
            }
        #endif
    }
}