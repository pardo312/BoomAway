using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetTimeLeftBomb : MonoBehaviour
{
    private BoomAway.Assets.Scripts.Game.Player.Guns.Bomb bombScript;
    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Bomb(Clone)") != null)
            {
                bombScript = GameObject.Find("Bomb(Clone)").GetComponent<BoomAway.Assets.Scripts.Game.Player.Guns.Bomb>();
                GetComponent<TextMeshProUGUI>().text = Mathf.Ceil(bombScript.timeUntilExplode).ToString();
            }
    }
}
