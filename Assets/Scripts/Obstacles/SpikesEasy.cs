using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpikesEasy : MonoBehaviour
{
    [SerializeField] private GameObject spikes; // parent of spike colliders
    [SerializeField] private Tilemap noSpikes;
    [SerializeField] private Tilemap withSpikes;
    [SerializeField] private GameObject display;
    private int spikesHit;
    [SerializeField] private int maxSpikesHit;

    [SerializeField] private GameObject player;

    private bool changeScene = false;
    public bool onSpike = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (display.activeSelf || onSpike )
        {
            player.GetComponent<PlayerMovement>().canMove = false;
        }
        else
        {
            player.GetComponent<PlayerMovement>().canMove = true;
        }

        if (spikesHit >= maxSpikesHit && !changeScene)
        {
            
            StartCoroutine("WaitToDeactivate");
        }
        /*else if (spikesHit >= maxSpikesHit * 2 && !changeScene)
        {
            display.SetActive(true);
            StartCoroutine("WaitToDeactivate");
        }
        else if (spikesHit >= maxSpikesHit * 3 && !changeScene)
        {
            display.SetActive(true);
            StartCoroutine("WaitToDeactivate");
        }
        else if (spikesHit >= maxSpikesHit * 4 && !changeScene)
        {
            display.SetActive(true);
            StartCoroutine("WaitToDeactivate");
        }*/
    }

    public void hitSpike()
    {
        spikesHit++;
    }

    public void ChangeIt()
    {
        changeScene = true;
        //switchMap();
        StartCoroutine("WaitToSwitchTilemap");
    }

    public void ResetCount()
    {
        display.SetActive(false);
        Debug.Log("HIIIIIIIIIIIIIII");
        //player.GetComponent<PlayerMovement>().canMove = true;
        spikesHit = 0;
    }

    public void TurnOffDisplay()
    {
        display.SetActive(false);
    }

    IEnumerator WaitToDeactivate()
    {
        yield return new WaitForSeconds(2.3f);
        display.SetActive(true);
        //player.gameObject.GetComponent<PlayerMovement>().canMove = false;
    }
    IEnumerator WaitToSwitchTilemap()
    {
        
        display.SetActive(false);
        yield return new WaitForSeconds(2f);
        spikes.SetActive(false);
        noSpikes.gameObject.SetActive(true);
        withSpikes.gameObject.SetActive(false);
       //player.GetComponent<PlayerMovement>().canMove = true;
    }

    public void switchMap()
    {
        display.SetActive(false);
        spikes.SetActive(false);
        noSpikes.gameObject.SetActive(true);
        withSpikes.gameObject.SetActive(false);
        //player.GetComponent<PlayerMovement>().canMove = true;
    }

}
