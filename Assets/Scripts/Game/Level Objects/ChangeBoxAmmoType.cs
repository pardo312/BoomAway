using UnityEngine;
using UnityEngine.UI;

public class ChangeBoxAmmoType : MonoBehaviour
{
   public void ChangeBoxAmmoTypeUp(){
       if(Grid.gameStateManager.currentBoxAmmoType < Constants.MAX_AMMO-2){
           Debug.Log(Grid.gameStateManager.currentBoxAmmoType);
           Grid.gameStateManager.currentBoxAmmoType++;
           transform.parent.GetComponent<Image>().sprite = Grid.gameStateManager.ammoTypeSprites[Grid.gameStateManager.currentBoxAmmoType];
       }
   }
   public void ChangeBoxAmmoTypeDown(){
       if(Grid.gameStateManager.currentBoxAmmoType >0){
           Debug.Log(Grid.gameStateManager.currentBoxAmmoType);
           Grid.gameStateManager.currentBoxAmmoType--; 
           transform.parent.GetComponent<Image>().sprite = Grid.gameStateManager.ammoTypeSprites[Grid.gameStateManager.currentBoxAmmoType];
       }
   }
}
