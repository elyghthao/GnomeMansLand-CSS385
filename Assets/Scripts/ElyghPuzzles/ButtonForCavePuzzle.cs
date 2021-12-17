using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonForCavePuzzle : MonoBehaviour
{

    [SerializeField] private GameObject horisontalWood;
    [SerializeField] private GameObject verticalWood;
    [SerializeField] private GameObject redButton;
    [SerializeField] private LockAnimated door;
    [SerializeField] private float distance;
    [SerializeField] private float speed;
    private double currentPos;
    private Vector2 startPos;

    [SerializeField] private Camera cam;
    private bool canPress = true;
    private bool onRightSide = false;
    private bool boxIsDropped = false;

    private bool verticalDoorisDropped = false;
    private bool notPressedYet = true;
    // Start is called before the first frame update
    void Start()
    {
        door.OpenLock();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Can press is: " + canPress);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player") && canPress)//this logic is flawed!!!
        {
            //Debug.Log("In here");
            if (Input.GetKey(KeyCode.E) && canPress)
            {
                canPress = false;
                StartCoroutine("dropDoor");
                //Debug.Log("BUTTON PRESSED");
                
                StartCoroutine("redButtonPressed");

                StartCoroutine("moveHorizontalWood");
            }
        }
    }


    IEnumerator dropDoor()
    {
        float currentPosV = verticalWood.transform.position.y;
        
        if(!verticalDoorisDropped)
        {
            Debug.Log("entered here");
            while (currentPosV - verticalWood.transform.position.y < 2.3)
            {
                Debug.Log("Moving vertical Door");
                //this is backwards because it is rotated 
                verticalWood.transform.Translate(-1 * speed * Time.deltaTime, 0, 0);
                yield return new WaitForEndOfFrame();
            }
            verticalDoorisDropped = true;
        }
        

    }
    IEnumerator moveHorizontalWood()
    {
        if (!boxIsDropped)
        {
            door.CloseLock();
            StartCoroutine("changeCam", 20);
            boxIsDropped = true;
        }
        currentPos = horisontalWood.transform.position.x;
        if(onRightSide)
        {
            while (horisontalWood.transform.position.x > (currentPos - distance))
            {
                horisontalWood.transform.Translate(-1f * speed * Time.deltaTime, 0, 0);
                yield return new WaitForEndOfFrame();
            }
        }else
        {
            while (horisontalWood.transform.position.x < (currentPos + distance))
            {
                horisontalWood.transform.Translate(01f * speed * Time.deltaTime, 0, 0);
                yield return new WaitForEndOfFrame();
            }
        }
        onRightSide = !onRightSide;

        yield return new WaitForSeconds(.5f);
        if(notPressedYet)
        {
            yield return new WaitForSeconds(1.5f);
            notPressedYet = false;
        }
        StartCoroutine("changeCam", 7);
        canPress = true;




    }
   
    IEnumerator changeCam(double newCamSize)
    {
        while (cam.orthographicSize < newCamSize)
        {
            cam.orthographicSize += .05f;
            yield return new WaitForEndOfFrame();
        }

        while (cam.orthographicSize > newCamSize)
        {
            cam.orthographicSize -= .05f;
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator redButtonPressed()
    {
        Vector3 temp = redButton.transform.localPosition;
        temp.y = .4f;
        redButton.transform.localPosition = temp;
        yield return new WaitForSeconds(.9f);
        temp.y = .6f;
        redButton.transform.localPosition = temp;
    }
}
