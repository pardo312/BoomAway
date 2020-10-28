using UnityEngine;
using TMPro;
using System.Collections;

public class SaveGameAs : MonoBehaviour
{
    public TMP_InputField saveName;
    private GameObject pausebutton;

    public void saveGameAs(){
        transform.GetChild(0).gameObject.SetActive(false);
        pausebutton = GameObject.Find("PauseButton");
        if(pausebutton!=null)
            pausebutton.SetActive(false);
        StartCoroutine(captureScreenshot());
    }

    IEnumerator captureScreenshot()
    {  
        yield return new WaitForEndOfFrame();
        
        string imagePath = Application.persistentDataPath + "/image.png";
        //about to save an image capture
        Texture2D screenImage = new Texture2D(Screen.width, Screen.height);
        //Get Image from screen
        screenImage.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenImage.Apply();

        transform.GetChild(0).gameObject.SetActive(true);
        if(pausebutton!=null)
            pausebutton.SetActive(true);
        //Convert to png
        byte[] imageBytes = screenImage.EncodeToPNG();

        Grid.worldSaveManager.saveWorld(saveName.text,imageBytes);
    }
}
