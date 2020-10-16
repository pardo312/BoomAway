using UnityEngine;

public class PickUpAmmoAnim : MonoBehaviour
{
    void Update()
    {
        if(transform.position.y < 1)
            transform.position += new Vector3(0,0.02f,0);
        else{
            Destroy(gameObject,0.5f);
        }
    }
}
