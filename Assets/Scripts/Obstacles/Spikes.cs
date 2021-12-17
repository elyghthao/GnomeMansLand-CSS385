using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public GameObject newBoundMin; //Min Game Bound if new location is a new level
    public GameObject newBoundMax; //Max Game Bound if new location is a new level
    public GameObject playerNewStart; //Position player should move to
    public GameObject cameraNewStart; //Position cam should move to
    public bool outLevel; //true if the player will move to a location outside the level area
    private CamSupport myCam;
    private CamMovement camMove;
    private bool canActivate = false;
    private Collider2D playerCollider;
    [SerializeField] private GameObject player;

    [SerializeField] private float waitTime;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        myCam = Camera.main.GetComponent<CamSupport>();
        camMove = player.GetComponent<CamMovement>();
        playerCollider = player.gameObject.transform.Find("circle collide").GetComponent<Collider2D>();
        anim = player.gameObject.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        /*if (canActivate && Input.GetKeyUp(KeyCode.E))
        {
            //Debug.Log("Portal Update yay");
            teleport();
        }*/
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == playerCollider)
        {
            // UP SPIKES HIT
            if (player.GetComponentInChildren<SpikesEasy>() != null)
            {
                player.GetComponentInChildren<SpikesEasy>().hitSpike();
                player.GetComponentInChildren<SpikesEasy>().onSpike = true;
            }
            
            Debug.Log("Portal OnTriggerEnter2D");
            //if not with touch, do nothing if E is not pressed
            canActivate = true;
            StartCoroutine("WaitToTeleport");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == playerCollider)
        {
            // UP SPIKES HIT
            canActivate = false;
            Debug.Log("Portal OnTriggerExit2D");
        }
    }
    private void teleport()
    {
        //move player; ignore z
        player.transform.position = new Vector3(playerNewStart.transform.position.x,
            playerNewStart.transform.position.y, player.transform.position.z);
        //change camera bounds if leaving level area
        if (outLevel)
        {
            myCam.GameBound_Min = newBoundMin;
            myCam.GameBound_Max = newBoundMax;
            StartCoroutine("fasterLerp");
        }
        //move camera; ignore z
        myCam.transform.position = new Vector3(cameraNewStart.transform.position.x,
            cameraNewStart.transform.position.y, myCam.transform.position.z);
        if (player.GetComponentInChildren<SpikesEasy>() != null)
        {
            
            player.GetComponentInChildren<SpikesEasy>().onSpike = false;
        }
    }
    //While moving the cam to the area in the next level, 
    IEnumerator fasterLerp()
    {
        myCam.SetLerpParameters(1,1);
        while (!myCam.inGameBounds())
        {
            yield return null;
        }
        myCam.SetLerpParameters(2, 4);
    }

    IEnumerator WaitToTeleport()
    {
        player.gameObject.GetComponent<PlayerMovement>().canMove = false;
        player.gameObject.GetComponent<PlayerGnomeAnimation>().hasHitSpike = true;
        // wait for waitime, then teleport player
        yield return new WaitForSeconds(waitTime);
        player.gameObject.GetComponent<PlayerGnomeAnimation>().hasHitSpike = false;
        player.gameObject.GetComponent<PlayerMovement>().canMove = true;
        teleport();
        
    }
}
