using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BD;

public class GameManager : BD.GameManagerBase
{
    public static GameManager instance;
    
    [SerializeField]
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public StateTag mainMenuState;
    public StateTag playingState;
    public int score;

    public Inventory cInventory;

    public override void Awake()
    {
        base.Awake();
        SetInstance();
        Debug.Log("set collectable inventory from game manager");
        SetCollectableInventory();
    }
    
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_WEBGL
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.ForceSoftware);
#else
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
#endif
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    SceneManager.LoadScene("Menu");
        //}
        /*if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("MainMenu");
        }*/

        //Debug.Log("Collectables: " + cInventory.getItems().Count);

        if (cInventory.getItems().Count == 12)
        {
            // SHOW POPUP
        }

    }

    public override void SetInstance()
    {
        if (!instance)
            instance = this;
        // else
        //     Debug.LogError($"GameManager: Trying to set instance but there is already one! {instance}");
    }

    public void SetCollectableInventory()
    {
        if (cInventory == null)
            cInventory = new Inventory();
    }

    public void AwardScore(int award)
    {
        score += award;
        // find UI, tell it to reward
        //collectables.Add(collectable);
        
    }

    //public 
}
