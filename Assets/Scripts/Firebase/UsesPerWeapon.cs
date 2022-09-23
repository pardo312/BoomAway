using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class UsesPerWeapon : MonoBehaviour
{
    [Header("ANALYTIC COMPONENT")]
    string urlFirebaseAnalytics = "https://boomaway-2ccf0-default-rtdb.firebaseio.com/Analytics/UsesPerWeapon";

    public void updateUse()
    {
        #if !UNITY_EDITOR
            string ammoType = "";

            switch (Grid.gameStateManager.currentAmmoType)
            {
                case 0:
                    ammoType = "Bomb";
                    break;
                case 1:
                    ammoType = "C4";
                    break;
                case 2:
                    ammoType = "Fast_Rocket";
                    break;
                case 3:
                    ammoType = "Slow_Rocket";
                    break;
                default:
                    break;
            }
       
            //Double Quotation
            string dQ = ('"' + "");

            string bodyJsonString = "{" + dQ + ammoType + dQ + ":" + (int)(1) + "}";

            var request = new UnityWebRequest(urlFirebaseAnalytics + ".json?auth="+Grid.gameStateManager.tokenFirebase, "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SendWebRequest();
        #endif

    }


}
