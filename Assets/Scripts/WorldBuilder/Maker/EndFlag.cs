using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using BoomAway.Assets.Scripts.Game.Player;

public class EndFlag : MonoBehaviour
{
    [SerializeField] private TimeOnLevel timeOnLevelScript;
    private bool alredyLoading = false;
    private Animator animator;
    private GameObject player;
    private bool initFadeIn;
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
                    timeOnLevelScript.uploadLevelCompletionTime();
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
            }
        }

    }
    void Update(){
        if(initFadeIn){
            player.GetComponent<PlayerMovement>().speed -= 3*Time.deltaTime;
            player.GetComponent<Animator>().speed -= Time.deltaTime*0.5f;
        }
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
