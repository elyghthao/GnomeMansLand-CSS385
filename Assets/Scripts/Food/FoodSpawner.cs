using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public enum FoodType
    {
        Apple,
        Cherry
    }
    public GameObject player; //our player
    private bool foodThere = true;

    private Rigidbody2D rb;
    private GameObject wingLeft, wingRight;

    private new AudioSource audio;

    //private CharacterController2D charController;
    //private PlayerMovement playerMove;

    void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
    }
    void Update()
    {
        //Debug.Log("foodThere is: " + foodThere);
    }

    private void SpawnApple()
    {
        GameObject apple = Instantiate(Resources.Load("Prefabs/Food/Apple") as GameObject);
        apple.transform.localPosition = transform.localPosition;
        apple.transform.GetChild(0).GetComponent<FoodBehavior>().spawner = this;
    }

    public void applyEffect(FoodType foodType)
    {
        switch (foodType)
        {
            case FoodType.Apple:
                StartCoroutine("AppleEffect");
                break;
            //case FoodType.Cherry:
            //    StartCoroutine("CherryEffect");
            //    break;
            default:
                return;
        }
    }

    public IEnumerator AppleEffect()
    {
        rb.drag = 2;
        for (int i = 0; i < 6; i++)
        {
            //Debug.Log("AppleEffect: " + i);
            yield return new WaitForSeconds(1f);
        }
        rb.drag = 0;
        //destroy wings
        Destroy(wingLeft);
        Destroy(wingRight);
        SpawnApple();
        
    }

    public void setWings(GameObject wingL, GameObject wingR)
    {
        wingLeft = wingL; 
        wingRight = wingR;
        //give wings to player
        wingLeft.transform.parent = player.transform;
        wingRight.transform.parent = player.transform;
    }
    //public IEnumerator AppleEffect()
    //{
    //    float orig = charController.m_JumpForce;
    //    charController.m_JumpForce = 800f;
    //    for (int i = 0; i < 10; i++)
    //    {
    //        Debug.Log("AppleEffect: " + i);
    //        yield return new WaitForSeconds(1f);
    //    }
    //    charController.m_JumpForce = orig;
    //}

    //IEnumerator CherryEffect()
    //{
    //    float orig = playerMove.runSpeed;
    //    playerMove.runSpeed = 80f;
    //    for (int i = 0; i < 10; i++)
    //    {
    //        Debug.Log("CherryEffect: " + i);
    //        yield return new WaitForSeconds(1f);
    //    }
    //    playerMove.runSpeed = orig;
    public void playSound()
    {
        audio.Play();
    }
}


