using UnityEngine;

public class ManageAmmo : MonoBehaviour
{
    [SerializeField] private GameObject[] gunsPrefab;
    [HideInInspector]

    // Update is called once per frame
    void Update()
    {
        int ammoType = Grid.gameStateManager.currentAmmoType;

        if(!Grid.gameStateManager.hasCurrentAmmo){
            if(Grid.gameStateManager.currentAmmo[ammoType]>0){
                Grid.gameStateManager.hasCurrentAmmo=true;
                var currentAmmo = Instantiate(gunsPrefab[ammoType], transform);
                currentAmmo.transform.localPosition = Vector3.zero;
            }
        }
    }
}
