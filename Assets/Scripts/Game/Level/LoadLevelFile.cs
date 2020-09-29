using UnityEngine;

public class LoadLevelFile : MonoBehaviour
{
    [SerializeField]private bool needToLoadLevel;
    // Start is called before the first frame update
    void Start()
    {
        Grid.gameStateManager.editing = false;
        loadLevel();
        
    }
    void loadLevel(){
        if(needToLoadLevel){
            Grid.worldSaveManager.loadWorldFromFirebase(Grid.gameStateManager.currentLevel,SaveType.Story);
        }
           

    }
   
}
