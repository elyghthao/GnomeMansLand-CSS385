using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackedBlock : MonoBehaviour
{
    public GameObject player;
    AudioSource audio;
    private Vector3 curPos;
    private Vector3 newPos;
    private bool alreadyStarted = false;
    private bool isShaking = false;
    private bool canSpawn = false;

    private Transform platform;
    private Transform platformCracked;

    // Start is called before the first frame update
    void Start()
    {
        curPos = this.gameObject.transform.position;
        newPos = curPos;
        newPos.x = 10000000;

        platform = transform.Find("Platform");
        platformCracked = transform.Find("PlatformCracked");


    }

    // Update is called once per frame
    void Update()
    {
        
        if (player != null && Vector3.Distance(curPos, player.transform.position) > 2f)
        {
            canSpawn = true;
        }else
        {
            canSpawn = false;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !alreadyStarted)
        {
            Vector2 direction = collision.GetContact(0).normal;
            if(direction.y == -1)
            {
                alreadyStarted = true;
                StartCoroutine("disappear");
            }
        }
    }

    IEnumerator disappear()
    {
        if (audio != null)
            audio.Play(0);
        platform.gameObject.SetActive(false);
        platformCracked.gameObject.SetActive(true);

        yield return new WaitForSeconds(.10f);
        Vector3 sub = curPos;
        Debug.Log("Started disappear");
        yield return new WaitForSeconds(.2f);
        
        StartCoroutine("startTimer");
        while (isShaking)
        {
            Debug.Log("shaking object");
            
            sub.x = curPos.x+ Mathf.Sin(Time.time * 100) * .1f;
            transform.position = sub;
            yield return new WaitForEndOfFrame();
        }
        transform.position = newPos;
        yield return new WaitForSeconds(2f);

        yield return new WaitUntil(() => canSpawn);
        
        platform.gameObject.SetActive(true);
        platformCracked.gameObject.SetActive(false);
        transform.position = curPos;
        alreadyStarted = false;
    }

    IEnumerator startTimer()
    {
        isShaking = true;
        yield return new WaitForSeconds(.2f);
        isShaking = false;
    }
}
