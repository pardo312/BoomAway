using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageLifeBar : MonoBehaviour
{
    private Image lifeBarValueImg;
    // Start is called before the first frame update
    void Start()
    {
        lifeBarValueImg = this.gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        lifeBarValueImg.fillAmount = Grid.gameStateManager.health;
    }
}
