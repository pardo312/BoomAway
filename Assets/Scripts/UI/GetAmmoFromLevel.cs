using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetAmmoFromLevel : MonoBehaviour
{
    [Tooltip("Types of guns: \n 0 = Bomb \n 1 = C4")]
    [SerializeField]private int ammoType;
    private TextMeshProUGUI textMP;
    // Start is called before the first frame update
    void Start()
    {
        textMP = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        textMP.text = Grid.gameStateManager.ammo[ammoType].ToString();
    }
}
