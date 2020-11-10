using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using BoomAway.Assets.Scripts.Game.Player;
using UnityEngine.Networking;
using System.Text;

public class EndFlag : MonoBehaviour
{
    [SerializeField] private TimeOnLevel timeOnLevelScript;
    private bool alredyLoading = false;
    private Animator animator;
    private GameObject player;
    private bool initFadeIn;
    private string urlFirebaseOnline = "https://boomaway-10de3.firebaseio.com/StoryLevels/";
    private string urlFirebaseOnlineLevels = "https://boomaway-10de3.firebaseio.com/OnlineLevels/";
    void Awake()
    {
        animator = GameObject.Find("FadeEffect").GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!alredyLoading)
        {
            if (collision.CompareTag("Player"))
            {
                if (SceneManager.GetActiveScene().name.Equals("StoryLevel") || SceneManager.GetActiveScene().name.Equals("Tutorial"))
                {
                    alredyLoading = true;
                    Grid.gameStateManager.IsOnStoryMode = true;
                    timeOnLevelScript.uploadLevelCompletionTime();
                    StartCoroutine(sendScore( Grid.gameStateManager.points));
                    switch (Grid.gameStateManager.currentLevel)
                    {
                        default:
                            Grid.gameStateManager.currentLevel = "LVL2";
                            break;
                        case "LVL2":
                            Grid.gameStateManager.currentLevel = "LVL3";
                            break;
                        case "LVL3":
                            Grid.gameStateManager.currentLevel = "LVL4";
                            break;
                        case "LVL4":
                            Grid.gameStateManager.currentLevel = "LVL5";
                            break;
                        case "LVL5":
                            Grid.gameStateManager.currentLevel = "LVL6";
                            break;
                        case "LVL6":
                            Grid.gameStateManager.currentLevel = "LVL7";
                            break;
                        case "LVL7":
                            Grid.gameStateManager.currentLevel = "LVL8";
                            break;
                        case "LVL8":
                            Grid.gameStateManager.currentLevel = "LVL1";
                            StartCoroutine(fadeIn("TitleScreen"));
                            break;

                    }
                    initFadeIn = true;
                    player = collision.gameObject;
                    Grid.gameStateManager.initRestart();
                    StartCoroutine(fadeIn("StoryLevel"));
                }
                else if(!SceneManager.GetActiveScene().name.Equals("WorldBuilder"))
                {
                    StartCoroutine(sendScore(Grid.gameStateManager.points));
                }
            }
        }

    }
    void Update(){
        if(initFadeIn){
            player.GetComponent<PlayerMovement>().speed -= 3*Time.deltaTime;
            player.GetComponent<Animator>().speed -= Time.deltaTime*0.5f;
        }
    }

    IEnumerator sendScore( float score)
    {
        UnityWebRequest request = null;
        if (Grid.gameStateManager.IsOnStoryMode)
        {
            
            request = new UnityWebRequest(urlFirebaseOnline + Grid.gameStateManager.currentLevel + "/Leaderboard/" + Grid.gameStateManager.usernameOnline + ".json?auth=" + Grid.gameStateManager.tokenFirebase, "POST");
        }
        else {
            string level = Grid.gameStateManager.currentOnlineLevel;
            request = new UnityWebRequest(urlFirebaseOnlineLevels + level + "/Leaderboard/" + Grid.gameStateManager.usernameOnline + ".json?auth=" + Grid.gameStateManager.tokenFirebase, "POST");
        }

        Debug.Log(((int)score).ToString());
        Debug.Log(request.url);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(((int)score).ToString());
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        Debug.Log(request.result);

        //Intento anterior
        /*using (UnityWebRequest webRequest = UnityWebRequest.Post(urlFirebaseOnline + level + "/Leaderboard/" + Grid.gameStateManager.usernameOnline + ".json", form))
        {
            webRequest.SetRequestHeader("Content-Type", "application/json");
            
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.Log(urlFirebaseOnline + "/" + level + "/Leaderboard/" + Grid.gameStateManager.usernameOnline + ".json");
                Debug.Log(webRequest.error);
            }
            else
            {
                Debug.Log(urlFirebaseOnline + "/" + level + "/Leaderboard/" + Grid.gameStateManager.usernameOnline + ".json");
                Debug.Log("No errors");
            }
            Debug.Log(webRequest.result);
        }*/
    }

    IEnumerator fadeIn(string sceneName){    
        animator.SetBool("FadeIn",true);
        animator.SetBool("FadeOut",false);
        yield return new WaitForSeconds(2);
        initFadeIn = false;
        player.GetComponent<PlayerMovement>().speed = Constants.PLAYER_SPEED;
        player.GetComponent<Animator>().speed =1;
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }
}
