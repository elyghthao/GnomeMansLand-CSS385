using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOneTime : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private CandyCaneDoor candy;
    private bool buttonOn = false;
    private bool canPress = false;
    private bool alreadyPressed = false;
    private Transform popup;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canPress && Input.GetKeyDown(KeyCode.E))
        {
            buttonOn = !buttonOn;

            if (buttonOn)
                SwitchOn();
            // else
            //     SwitchOff();
            
        }
    }

    public void SwitchOn()
    {
        Debug.Log("button on");
        animator.SetBool("buttonOn", true);
        candy.Open();
        alreadyPressed = true;
        canPress = false;
        popup = transform.Find("ePopup");
        popup.gameObject.SetActive(false);
    }
    // public void SwitchOff()
    // {
    //     Debug.Log("button off");
    //     animator.SetBool("On", false);
    //     candy.Close();
    // }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Found button collision");
        if (!alreadyPressed)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                canPress = true;
                popup = transform.Find("ePopup");
                popup.gameObject.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!alreadyPressed)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                canPress = false;
                popup = transform.Find("ePopup");
                popup.gameObject.SetActive(false);
            }
        }
    }
}
