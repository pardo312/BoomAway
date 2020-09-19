using UnityEngine;

public class isAGameScene : MonoBehaviour
{
    [SerializeField]private bool isGameScene;
    // Start is called before the first frame update
    void Awake()
    {
        Grid.gameStateManager.IsOnGame = isGameScene;
        
        AnalyticsResult result = Analytics.CustomEvent("Test");
        // This should print "Ok" if the event was sent correctly.
        Debug.Log(result);
    }
}
