using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAllLeftOverAmmo : MonoBehaviour
{
    public void destroyAllLeftoverAmoo(){
        GameObject[] ammo = GameObject.FindGameObjectsWithTag("Explosive");
        foreach(GameObject i in ammo)
        GameObject.Destroy(i);
    }
}
