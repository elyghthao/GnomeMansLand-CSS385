using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGnomeAnimation : MonoBehaviour
{
    [SerializeField] CharacterController2D controller;
    public LadderMovement ladder;
    AudioSource audio;
    [HideInInspector] public bool hasHitSpike = false;
    private bool isDancing = false;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine("Dances");
        }




        //Debug.Log("Ladder is:" + ladder.isClimbing);
        //Debug.Log("moveSpeed is: " + controller.moveSpeed);
        if(isDancing)
        {
            GetComponent<Animator>().Play("Dance1");
            
        }
        else if (hasHitSpike)
        {
            GetComponent<Animator>().Play("hitSpike");

        }
        else if(ladder.isClimbing)
        {
            GetComponent<Animator>().Play("Ladder");
            if (Input.GetAxis("Vertical") != 0)
            {
                GetComponent<Animator>().enabled = true;
            }else
            {
                GetComponent<Animator>().enabled = false;
            }
        }
        else if (controller.isPushing)
        {
            if (controller.moveSpeed != 0)
                GetComponent<Animator>().Play("Pushing");
            else
                GetComponent<Animator>().Play("PushingIdle");
        }
        else if (!controller.m_Grounded)
        {
            GetComponent<Animator>().Play("Jumping");

        }else if (controller.moveSpeed == 0 && controller.m_Grounded)
        {
            GetComponent<Animator>().Play("Idle");
        }
        else if (controller.moveSpeed != 0 && controller.m_Grounded)
        {
            GetComponent<Animator>().Play("Running");
        }
    }

    IEnumerator Dances()
    {
        isDancing = true;
        GetComponent<PlayerMovement>().canMove = false;
        yield return new WaitForSeconds(4);//4
        isDancing = false;
        GetComponent<PlayerMovement>().canMove = true;
    }


   
}
