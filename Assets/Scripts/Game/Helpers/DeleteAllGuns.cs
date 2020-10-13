using UnityEngine;

public class DeleteAllGuns : MonoBehaviour
{
    public void deleteAllGuns()
    {
        GameObject[] guns = GameObject.FindGameObjectsWithTag("Explosive");
        foreach(GameObject gun in guns)
            GameObject.Destroy(gun);
    }
}
