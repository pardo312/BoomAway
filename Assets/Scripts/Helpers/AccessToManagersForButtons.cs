using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BoomAway.Assets.Scripts.Helpers
{
    public class AccessToManagersForButtons : MonoBehaviour
    {
        public void saveGame(string path, object data)
        {
            Grid.saveGameManger.saveGame(path, data);
        }
        public void changeEditorMode()
        {
            Grid.gameStateManager.changeEditorMode();
        }
    }
}

