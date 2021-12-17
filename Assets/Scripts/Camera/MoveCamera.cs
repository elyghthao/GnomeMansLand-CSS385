using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{

    public GameObject target;
    public float waitTime;
    public GameObject player;
    public GameObject playerBody;
    private PlayerMovement playerMov;
    private GrabController grab;
    // Start is called before the first frame update
    private Vector3 currentPos;
    private Vector3 newPos;

    public bool playOnce = false;
    private bool hasBeenPlayed = false;
    private bool hasBeenTele = false;
    void Start()
    {
        playerMov = player.GetComponent<PlayerMovement>();
        grab = player.GetComponent<GrabController>();
        newPos = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasBeenTele && player != null)
        {
            player.gameObject.transform.position = newPos;
        }

    }

    public void activateSwitch()
    {
        if (playOnce && !hasBeenPlayed)
        {
            hasBeenPlayed = true;
            StartCoroutine("moveCam");
        }
        else if (!playOnce)
        {
            StartCoroutine("moveCam");
        }
    }

    IEnumerator moveCam()
    {
        
        
        playerMov.canMove = false;
        currentPos = player.gameObject.transform.position;
        yield return new WaitUntil(() => playerMov.canMove == false);

        if(grab.isHolding)
        {
            grab.letGo = true;
            yield return new WaitUntil(() => grab.letGo == true); 
            yield return new WaitForSeconds(.15f);
        }



        
        playerBody.SetActive(false);
        player.gameObject.transform.position = newPos;
        hasBeenTele = true;

        yield return new WaitForSeconds(waitTime);
        hasBeenTele = false;

        
        player.gameObject.transform.position = currentPos;
        yield return new WaitUntil(() => currentPos == player.gameObject.transform.position);
        playerMov.canMove = true;
        playerBody.SetActive(true);



    }



    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == target.name)
        {

            activateSwitch();

        }
    }
}
