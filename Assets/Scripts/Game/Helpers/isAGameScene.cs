using UnityEngine;

public class isAGameScene : MonoBehaviour
{
    [SerializeField]private bool isGameScene;
    // Start is called before the first frame update
    void Awake()
    {
        Grid.gameStateManager.IsOnGame = isGameScene;
    }
}
