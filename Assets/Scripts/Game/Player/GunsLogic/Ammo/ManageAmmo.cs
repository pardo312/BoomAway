using UnityEngine;

public class ManageAmmo : MonoBehaviour
{
    [SerializeField] private GameObject[] gunsPrefab;
    private int ammoType;

    private void Awake() {
        ammoType = Grid.gameStateManager.currentAmmoType;
    }
    // Update is called once per frame
    void Update()
    {
        int localAmmoType  = Grid.gameStateManager.currentAmmoType;

        if(localAmmoType != ammoType)
        {
            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            ammoType = localAmmoType;
            Grid.gameStateManager.hasCurrentAmmo=false;
        }
        if(!Grid.gameStateManager.hasCurrentAmmo){
            if(Grid.gameStateManager.currentAmmo[ammoType]>0){
                Grid.gameStateManager.hasCurrentAmmo=true;
                var currentAmmo = Instantiate(gunsPrefab[ammoType], transform);
                currentAmmo.transform.localPosition = Vector3.zero;
            }
        }
    }
}
