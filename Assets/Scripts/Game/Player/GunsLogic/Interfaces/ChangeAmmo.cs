using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAmmo : MonoBehaviour
{
    public void addAmmoOfType(int indexOfBomb){
        if(Grid.gameStateManager.ammo[indexOfBomb]<5)
            Grid.gameStateManager.ammo[indexOfBomb] ++;
    }
    public void removeAmmoOfType(int indexOfBomb){
        if(Grid.gameStateManager.ammo[indexOfBomb]>0)
            Grid.gameStateManager.ammo[indexOfBomb] --;
    }
}
