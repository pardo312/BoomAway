using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BoomAway.Assets.Scripts.Helpers
{
    public class AccessToManagersForButtons : MonoBehaviour
    {
        public void saveGame(string path)
        {
            Grid.saveGameManger.saveGame(path);
        }
        public void loadGame(string path)
        {
            Grid.saveGameManger.loadGame(path);
        }
    }
}

