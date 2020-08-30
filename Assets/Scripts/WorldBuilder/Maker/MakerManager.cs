using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakerManager : MonoBehaviour
{
    private MakerTile[] tiles;
    [SerializeField]private GameObject buttonPrefab;
    [SerializeField]private Transform layout;
    [SerializeField]private SpriteRenderer preview;
    [SerializeField]private GameObject[] hideOnEditObjects;
    int id;
    // Start is called before the first frame update
    void Awake()
    {
        tiles= Grid.worldSaveManager.makerTilePrefab;
        Grid.gameStateManager.editing = true;
        for (int i = 0; i < tiles.Length; i++)
        {
            int u = i;
            var t = Instantiate(buttonPrefab, layout);
            t.GetComponent<Image>().sprite = tiles[u].sprite;
            t.GetComponent<Button>().onClick.AddListener(()=>
            {
                id = u;
                preview.sprite = tiles[u].sprite;
            });
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        

        if(UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            return;
        if(!Grid.gameStateManager.editing)
            return;
        
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z=0;
        pos.x = Mathf.RoundToInt(pos.x);
        pos.y = Mathf.RoundToInt(pos.y);

        preview.transform.position = pos;

        var c = Physics2D.CircleCast(pos,0.4f,Vector2.zero);

        if(Input.GetKey(KeyCode.Mouse0))
        {
            if(c.collider == null)
                Instantiate(tiles[id].gameObject,pos,Quaternion.identity);
        }
        if(Input.GetKey(KeyCode.Mouse1))
        {
            if(c.collider != null)
                Destroy(c.collider.gameObject);
        }
    }
    public void changeEditorMode()
    {
        bool state = Grid.gameStateManager.editing;
        Grid.gameStateManager.editing = !state ;
        preview.enabled = !state;
        for (int i = 0; i < hideOnEditObjects.Length; i++)
        {
            hideOnEditObjects[i].SetActive(!state);
        }
    }
}
