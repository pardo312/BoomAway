using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeUsernameUI : MonoBehaviour
{
    
    private TextMeshProUGUI userName;
    // Start is called before the first frame update
    void Update()
    {
        userName = GetComponent<TextMeshProUGUI>();
        userName.SetText(Grid.gameStateManager.usernameOnline);
    }

}
