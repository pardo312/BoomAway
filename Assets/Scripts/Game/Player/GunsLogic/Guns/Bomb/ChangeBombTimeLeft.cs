using UnityEngine;
using TMPro;

    public class ChangeBombTimeLeft : MonoBehaviour
    {
        private BoomAway.Assets.Scripts.Game.Player.Guns.Bomb bombScript;
        private bool found = false;
        Quaternion rotation;
        void Update()
        {
            if (GameObject.Find("Bomb(Clone)") != null)
            {
                found = true;
                bombScript = GameObject.Find("Bomb(Clone)").GetComponent<BoomAway.Assets.Scripts.Game.Player.Guns.Bomb>();
                if (bombScript.readyToExplode && !Grid.gameStateManager.editing)
                {
                    if (TryGetComponent<TextMeshPro>(out TextMeshPro tmp))
                    {
                        transform.rotation = Quaternion.identity;
                        tmp.text = Mathf.Ceil(bombScript.timeUntilExplode).ToString();
                    }
                }
            }
            else{
                found = false;
            }
            
        }

        public void addTime()
        {
            if (found && bombScript.timeUntilExplode < Constants.BOMB_MAX_TIME)
                bombScript.timeUntilExplode++;
        }
        public void removeTime()
        {
            if ( found && bombScript.timeUntilExplode  > 0)
                bombScript.timeUntilExplode--;
        }
    }

