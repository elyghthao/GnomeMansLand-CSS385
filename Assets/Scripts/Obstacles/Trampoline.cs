using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    private Animator animator;
    AudioSource audio;
    private GameObject jumper = null;
    private bool canJump = true;
    //public GameObject player;
    [SerializeField] private float jumpForce = 400f;
    [SerializeField] private float jumpMultiplier = 1f;
    [SerializeField] private float movableMumliplier = 1f;

    public int jumpCount = 0;

    // Start is called before the first frame update
    void Awake()
    {
        audio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if(canJump && jumper != null)
        {
            canJump = false;
            if (jumper.tag == "Movable")
            {
                Debug.Log("Bounce Box");
                jumper.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce*2 * jumpMultiplier * movableMumliplier));
                jumper = null;
            }
            else if (jumper.tag == "Player")
            {
                //jumpCount++;
                //Debug.Log("trampoline: " + jumpCount);
                jumper.GetComponent<Animator>().Play("Jumping", -1, 0f);
                jumper.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce*2 * jumpMultiplier));
                jumper = null;
                
            }
            canJump = true;
        }
    }

    // private void OnTriggerEnter2D(Collider2D collider)
    // {
    //     Rigidbody2D body = collider.aaattachedRigidbody;

    //     // no rigidbody
    //     if (body == null || body.isKinematic)
    //         return;
    //     if (collider.GetComponent<CharacterController2D>() != null)
    //     {
    //         Debug.Log("player is standing on trampoline");
    //         animator.SetBool("isStepped", true);
    //         body.AddForce(new Vector2(0f, jumpForce));
    //         // Jump(collider.gameObject);
    //     }

    // }
    // private void OnTriggerExit2D(Collider2D collider)
    // {
    //     animator.SetBool("isStepped", false);
    // }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Movable")
        {
            jumpCount++;
            //Debug.Log("trampoline: " + jumpCount);


            if (audio != null)
               audio.Play(0);
            if (animator != null)
                animator.Play("Trampoline Activate", -1, 0f);
            jumper = collision.gameObject;
        }

        // Debug.Log("enter trampoline");
        // animator.SetBool("isStepped", true);

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //Debug.Log("player is standing on trampoline");
        //if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Movable")
        //{
        //    if (audio != null)
        //        audio.Play(0);
        //    if (animator != null)
        //        animator.Play("Trampoline Activate", -1, 0f);
        //    jumper = collision.gameObject;
        //}




    }
   



    

}
