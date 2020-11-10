using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.IO;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using SimpleJSON;

public class LoadSavegameSlots : MonoBehaviour
{
    public GameObject loadButtonPrefab;
    public GameObject loadArea;
    public GameObject commentPrefab;
    public GameObject loadCommentArea;

    public GameObject Leaderboard;
    public GameObject ScorePrefab;
    public GameObject VerticalListArea;
    public LeaderBoardScript leaderBoardScript;

    public GameObject commentMenu;
    public GameObject onlineMenu;

    public CreateComment createComm;

    public GameObject loadingText;

    private List<string> author = new List<string>();
    private List<string> saveFiles = new List<string>();
    private List<string> saveUserOfFile = new List<string>();
    private List<string> savedComments = new List<string>();
    private List<int> upvotes = new List<int>();
    private List<int> downvotes = new List<int>();
    private List<int> votedUp = new List<int>();
    private List<int> votedDown = new List<int>();
    
    private List<string> thumbnail = new List<string>();

    private string templevel;

    private string urlFirebaseOnline = "https://boomaway-10de3.firebaseio.com/OnlineLevels";
    public void showLoadScreen()
    {
        saveFiles = new List<string>();
        for (int i = 0; i < loadArea.transform.childCount; i++)
        {
            Transform button = loadArea.transform.GetChild(i);
            Destroy(button.gameObject);
        }
        GetLoadFiles();
    }

    public void showCommentScreen(string level)
    {
        savedComments = new List<string>();
        for (int i = 0; i < loadCommentArea.transform.childCount; i++)
        {
            Transform button = loadCommentArea.transform.GetChild(i);
            Destroy(button.gameObject);
        }
        GetLoadComments(level);
    }

    public void showUserWorldsLoadScreen()
    {
        saveFiles = new List<string>();
        for (int i = 0; i < loadArea.transform.childCount; i++)
        {
            Transform button = loadArea.transform.GetChild(i);
            Destroy(button.gameObject);
        }
        GetUserWorldsLoadFiles();
    }


    public void GetLoadFiles()
    {
        StartCoroutine(UnityRequestLevelOnline());
    }
    public void GetUserWorldsLoadFiles()
    {
        StartCoroutine(UnityRequestUserLevelsOnline());
    }

    public void GetLoadComments(string level)
    {
        StartCoroutine(UnityRequestCommentsOnline( level));
    }

    IEnumerator UnityRequestCommentsOnline( string level) {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(urlFirebaseOnline + '/' + level + '/'  + "Comments" +".json"))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                JSONNode data = JSON.Parse(webRequest.downloadHandler.text);
                foreach (JSONNode comment in data)
                {
                    Debug.Log(comment);
                    savedComments.Add(comment);
                }
            }
        }
        for (int i = 0; i < savedComments.Count; i++)
        {

            GameObject buttonObject = Instantiate(commentPrefab);
            buttonObject.transform.SetParent(loadCommentArea.transform, false);
            buttonObject.GetComponentInChildren<TextMeshProUGUI>().text = savedComments[i];


        }
    }

    IEnumerator UnityRequestLevelOnline()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(urlFirebaseOnline + ".json"))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                JSONNode data = JSON.Parse(webRequest.downloadHandler.text);
                foreach (KeyValuePair<string, JSONNode> kvp in (JSONObject)data)
                {
                    saveUserOfFile.Add(kvp.Key);
                }
                foreach (JSONNode player in data)
                {
                    
                    saveFiles.Add(player["LevelName"]);
                    try
                    {
                        votedUp.Add(player["VotedUp"]);
                        votedDown.Add(player["VotedDown"]);
                        upvotes.Add(player["Upvotes"]);
                        downvotes.Add(player["Downvotes"]);
                    }
                    catch
                    {
                        upvotes.Add(0);
                        downvotes.Add(0);
                    }
                    try
                    {
                        author.Add(player["user"]);
                    }
                    catch
                    {
                        author.Add("Author");
                    }
                    StartCoroutine(addThumbnailToList(player["Thumbnail"]));
                }
            }
        }
        instatiateButton(false);
    }
    IEnumerator UnityRequestUserLevelsOnline() 
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(urlFirebaseOnline + ".json"))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                JSONNode data = JSON.Parse(webRequest.downloadHandler.text);
                foreach (KeyValuePair<string, JSONNode> kvp in (JSONObject)data)
                {
                    saveUserOfFile.Add(kvp.Key);
                }
                foreach(JSONNode player in data)
                {
                    if(player["user"].Equals(Grid.gameStateManager.usernameOnline))
                    {
                        saveFiles.Add(player["LevelName"]);
                        try
                        {
                            votedUp.Add(player["VotedUp"]);
                            votedDown.Add(player["VotedDown"]);
                            upvotes.Add(player["Upvotes"]);
                            downvotes.Add(player["Downvotes"]);
                        }
                        catch
                        {
                            upvotes.Add(0);
                            downvotes.Add(0);
                        }
                        try
                        {
                            author.Add(player["user"]);
                        }
                        catch
                        {
                            author.Add("Author");
                        }
                        StartCoroutine(addThumbnailToList(player["Thumbnail"]));
                        }
                }
            }      
        }  
        instatiateButton(true);      
    }
    IEnumerator addThumbnailToList(string thumbnailFirebase){
            yield return null;
            thumbnail.Add(thumbnailFirebase);
    }
    IEnumerator loadThumbnail(GameObject buttonObject, int index){
            yield return null;
            GameObject thumbnailImage = buttonObject.transform.GetChild(4).gameObject;
            Texture2D thumbnailTexture = new Texture2D(4, 2);
            byte[] thumbnailByte = System.Convert.FromBase64String(thumbnail[index]);
            thumbnailTexture.LoadImage(thumbnailByte);
            thumbnailTexture.Apply();
            thumbnailImage.GetComponent<RawImage>().texture =thumbnailTexture;
    }

    IEnumerator sendUpvote(OpenComments openCommentScript, string upvotes)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Put(urlFirebaseOnline + '/' + openCommentScript.level + '/' + "Upvotes.json?auth=" + Grid.gameStateManager.tokenFirebase, upvotes)) 
        {
            yield return webRequest.SendWebRequest();
        }
    }
    
    IEnumerator sendDownvote(OpenComments openCommentScript, string downvotes)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Put(urlFirebaseOnline + '/' + openCommentScript.level + '/' + "Downvotes.json?auth=" + Grid.gameStateManager.tokenFirebase, downvotes))
        {
            yield return webRequest.SendWebRequest();
        }
    }

    private void instatiateButton(bool isWorldBuilder)
    {
            for (int i = 0; i < saveFiles.Count; i++)
            {
            GameObject buttonObject = Instantiate(loadButtonPrefab);
            buttonObject.transform.SetParent(loadArea.transform,false);
            LoadScene loadSceneScript = buttonObject.AddComponent<LoadScene>();
            
            //Comments
            GameObject commentButton = buttonObject.transform.GetChild(2).gameObject;
            OpenComments openCommentScript = commentButton.GetComponent<OpenComments>();
            string templevel = saveUserOfFile[i];
            openCommentScript.level = templevel;
            openCommentScript.commentMenu = commentMenu;
            if(onlineMenu!= null)
                openCommentScript.onlineMenu = onlineMenu;
            openCommentScript.create = createComm;
            commentButton.GetComponent<Button>().onClick.AddListener(openCommentScript.showHide);
            commentButton.GetComponent<Button>().onClick.AddListener(delegate {
                showCommentScreen(templevel);
            });
            commentButton.GetComponent<Button>().onClick.AddListener(delegate { openCommentScript.updateLevel(); });
            //Author
            GameObject authors = buttonObject.transform.GetChild(1).gameObject;
            authors.GetComponent<TextMeshProUGUI>().text = author[i];

            //Votes
            GameObject upVoteButton = buttonObject.transform.GetChild(3).gameObject;
            upVoteButton.GetComponent<TextMeshProUGUI>().text = upvotes[i].ToString();

            //GameObject downVoteButton = buttonObject.transform.GetChild(4).gameObject;
            //downVoteButton.GetComponent<TextMeshProUGUI>().text = downvotes[i].ToString();
                string upvote = (upvotes[i] + 1).ToString();
                upVoteButton.GetComponent<TextMeshProUGUI>().text = upvotes[i].ToString();
                upVoteButton.GetComponent<Button>().onClick.AddListener(() =>
                {
                    StartCoroutine(sendUpvote(openCommentScript, upvote));
                });

            //LeaderBoardOnline
            GameObject leaderBoardButton = buttonObject.transform.GetChild(5).gameObject;
            leaderBoardButton.GetComponent<LeaderBoardScript>().level = templevel;
            leaderBoardButton.GetComponent<LeaderBoardScript>().LeaderBoard = Leaderboard;
            leaderBoardButton.GetComponent<LeaderBoardScript>().ScorePrefab = ScorePrefab;
            leaderBoardButton.GetComponent<LeaderBoardScript>().VerticalListArea = VerticalListArea;
            leaderBoardButton.GetComponent<Button>().onClick.AddListener(leaderBoardButton.GetComponent<LeaderBoardScript>().showOnlineLoadScreen);

            //Thumbnail
            StartCoroutine(loadThumbnail(buttonObject,i));
            //OnClickButton
            int index = i;
            if(isWorldBuilder)
            {
                buttonObject.GetComponent<Button>().onClick.AddListener(()=>
                {   
                    //Deshabilita el boton de back
                    GameObject.Find("BackButtonLoadWorld").SetActive(false);
                    Grid.gameStateManager.currentWorldBuilderLevel = saveFiles[index];
                    Time.timeScale=1;
                    loadSceneScript.loadScene("WorldBuilder");
                });
            }
            else{
                buttonObject.GetComponent<Button>().onClick.AddListener(() =>
                {
                    Grid.gameStateManager.currentOnlineLevel = saveFiles[index];
                    loadSceneScript.loadScene("OnlineLevel");
                });
            }
            //Button Text
            buttonObject.GetComponentInChildren<TextMeshProUGUI>().text = saveFiles[i];
            loadingText.SetActive(false);
        }
    }
}
