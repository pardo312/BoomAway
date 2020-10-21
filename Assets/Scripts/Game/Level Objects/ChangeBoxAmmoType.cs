using UnityEngine;
using UnityEngine.UI;

public class ChangeBoxAmmoType : MonoBehaviour
{
   public void ChangeBoxAmmoTypeUp(){
       if(Grid.gameStateManager.currentBoxAmmoType < Constants.MAX_AMMO-2){
           Grid.gameStateManager.currentBoxAmmoType++;
           transform.parent.GetComponent<Image>().sprite = Grid.gameStateManager.ammoTypeSprites[Grid.gameStateManager.currentBoxAmmoType];
           
       }
   }
   public void ChangeBoxAmmoTypeDown(){
       if(Grid.gameStateManager.currentBoxAmmoType >0){
           Grid.gameStateManager.currentBoxAmmoType--; 
           transform.parent.GetComponent<Image>().sprite = Grid.gameStateManager.ammoTypeSprites[Grid.gameStateManager.currentBoxAmmoType];
       }
   }
}
