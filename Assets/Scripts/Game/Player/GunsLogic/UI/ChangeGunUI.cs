using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGunUI : MonoBehaviour
{
    [Tooltip("0 = Bomb \n 1 = C4")]
    [SerializeField][Range(0,1)]private int gun;
    public void changeCurrentGunUI(){
        Grid.gameStateManager.currentAmmoType = gun;
    }
}
