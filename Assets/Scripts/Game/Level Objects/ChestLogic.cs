using UnityEngine;
using System.Collections.Generic;

public class ChestLogic : MonoBehaviour
{
    [SerializeField]private int typeOfammoToGive;
    [SerializeField]private GameObject AddAmmoAnimation;

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            if(Grid.gameStateManager.ammo[typeOfammoToGive]<Constants.MAX_AMMO)
                Grid.gameStateManager.currentAmmo[typeOfammoToGive]++;
            GameObject anim = Instantiate(AddAmmoAnimation,gameObject.transform);
            anim.transform.SetParent(null);
            anim.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Grid.gameStateManager.ammoTypeSprites[typeOfammoToGive];
            Destroy(gameObject);
        }
    }
}
