using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAmmo : MonoBehaviour
{
    public void addAmmoOfType(int indexOfBomb){
        if(Grid.gameStateManager.ammo[indexOfBomb]<Constants.MAX_AMMO)
            Grid.gameStateManager.ammo[indexOfBomb] ++;       
    }
    public void removeAmmoOfType(int indexOfBomb){
        if(Grid.gameStateManager.ammo[indexOfBomb]>0)
            Grid.gameStateManager.ammo[indexOfBomb] --;
    }
    private void Update() {
        
        if(Grid.gameStateManager.editing){
            for (int i = 0; i < Grid.gameStateManager.ammo.Length; i++)
            {
                Grid.gameStateManager.currentAmmo[i]= Grid.gameStateManager.ammo[i];
            }
        }
    }
}
