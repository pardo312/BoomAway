using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetAmmoFromLevel : MonoBehaviour
{
    
    [Tooltip("0 = Bomb \n 1 = C4 \n 2 = FR \n 3 = SR")]
    [SerializeField][Range(0,Constants.AMOUNT_GUNS-1)]private int ammoType;
    private TextMeshProUGUI textMP;
    // Start is called before the first frame update
    void Start()
    {
        textMP = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        textMP.text = Grid.gameStateManager.currentAmmo[ammoType].ToString();
    }
}
