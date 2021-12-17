using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyCaneDoor : MonoBehaviour
{
    private Animator animator;
    private new AudioSource audio;
    private bool audioPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
    public void Open()
    {
        Debug.Log("opening door");
        animator.SetBool("Open", true);
        if (audio != null)
        {
            if(Time.timeSinceLevelLoad > 5) //this is so it doesnt play it at the beginning of the game
            {
                if (!audioPlayed)
                {
                    audio.Play();
                    audioPlayed = true;
                }

            }
        }
    }
    public void Close()
    {
        animator.SetBool("Open", false);
    }
}
