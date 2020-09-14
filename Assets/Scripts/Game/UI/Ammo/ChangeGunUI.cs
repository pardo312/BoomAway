using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGunUI : MonoBehaviour
{
    [Tooltip("0 = Bomb \n 1 = C4 \n 2 = FR \n 3 = SR")]
    [SerializeField][Range(0,Constants.AMOUNT_GUNS-1)]private int gun;
    [SerializeField] private KeyCode weaponKey;
    private void Update() {
        if(Input.GetKeyDown(weaponKey)){
            Grid.gameStateManager.currentAmmoType = gun;
        }
    }
    public void changeCurrentGunUI(){
        Grid.gameStateManager.currentAmmoType = gun;
    }
}
