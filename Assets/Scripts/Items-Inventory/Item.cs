using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public partial class Item : MonoBehaviour
{
    [SerializeField] private Transform gnome;
    private float timeShow = 0.2f;
    private float timeRemaining = 0;
    private bool bridgeIsBuilt = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // if item is child of gnome, move with the gnome
        if (this.transform.parent == gnome)
        {
            MoveWithGnome();
        }

        CheckPickedUp();

        if (this.pickedup == true)
        {
            if ((Input.GetKey(KeyCode.Alpha1) && index ==1) 
                || (Input.GetKey(KeyCode.Alpha2) && index == 2)
                || (Input.GetKey(KeyCode.Alpha3) && index == 3))
            {
                if (!collectable)
                {
                    useItem();
                }
            }
        }
        // if (Input.GetKey(KeyCode.Alpha1) && this.pickedup == true && index == 1)
        // {
        //     // this.gameObject.GetComponent<SpriteRenderer>().enabled = true;//added this
        //     useItem();
        // }

        // if (Input.GetKey(KeyCode.Alpha2) && this.pickedup == true && index == 2)
        // {
        //     this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        //     useItem();
        // }

        // if (Input.GetKey(KeyCode.Alpha3) && this.pickedup == true && index == 3)
        // {
        //     this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        //     useItem();
        // }

        /*if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else if(pickedup)
        {
            StopUsing();
        }*/

    }

    public void MoveWithGnome()
    {
        // Follows position, but just in front
        this.transform.position = gnome.transform.position;
        Vector3 newpos = this.transform.position;
        if (gnome.transform.localScale.x < 1)
        {
            newpos.x -= .75f;
        }
        else
        {
            newpos.x += .75f;
        }

        newpos.y += .5f;
        this.transform.position = newpos;

        /*// Flips Sprite as needed
        if (gnome.transform.localScale.x < 0) // if gnome facing left, flip sprite
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else // if gnome facing right, dont flip
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }*/
    }

    public void CheckPickedUp()
    {
        //Debug.Log($"picked up: {this.pickedup}");
        //added this
        if (!this.pickedup)
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            //Debug.Log($"sprite off:");
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }//
    }

    public void useItem()
    {
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        
        //StartCoroutine("UseItem");
        if (this.itemType == ItemType.Spade)
        {
            UseSpade();
        }
        if (this.itemType == ItemType.WateringCan)
        {
            StartCoroutine("Watering");
        }
        if (this.itemType == ItemType.Hammer)
        {
            UseHammer();
        }
        if (this.itemType == ItemType.Key)
        {
            UseKey();
        }
    }

    public void StopUsing()
    {
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    IEnumerator Watering()
    {            
        int drops = Random.Range(3, 7);
        for (int i = 0; i < drops; i++)
        {
            GameObject water = Instantiate(Resources.Load("Prefabs/Items/waterdrop") as GameObject, this.transform);
            Vector3 pos = this.transform.position;

            float rand = Random.Range(.2f, .5f);
            if(gnome.localScale.x <0 )
                pos.x -= rand;
            else
                pos.x += rand;

            water.transform.position = pos;
            yield return null;
        }
    }

    private void UseSpade()
    {
        timeRemaining = timeShow;
        //audio[1].Play();*/

        Vector3 pos = transform.position;
        pos.y -= .5f;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(pos, 1f);
        // Debug.Log(hitColliders.Length);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("DigGround"))
            {
                Destroy(hitCollider.gameObject);
            }
        }
    }

    private void UseHammer()
    {
        
        Vector3 pos = transform.position;
        pos.y -= .5f;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(pos, .5f);
        // Debug.Log(hitColliders.Length);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Bridge"))
            {

                //hitCollider.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                //hitCollider.gameObject.transform.GetChild(1).gameObject.SetActive(true);
                if(!bridgeIsBuilt)
                {
                    hitCollider.gameObject.GetComponent<Animator>().Play("fixingBridge");
                    bridgeIsBuilt = true;
                    StartCoroutine("idlePlayer",(1.5f));
                }
            }
        }
    }

    private void UseKey()
    {
        timeRemaining = timeShow;
        //audio[1].Play();*/

        Vector3 pos = transform.position;
        pos.y -= .5f;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(pos, 1f);
        // Debug.Log(hitColliders.Length);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Lock"))
            {
                //Destroy(hitCollider.gameObject);
                hitCollider.gameObject.GetComponent<Animator>().Play("OpenLock");
            }
        }
    }

    IEnumerator idlePlayer(float time)
    {
        gnome.gameObject.GetComponent<PlayerMovement>().canMove = false;
        yield return new WaitForSeconds(time);
        gnome.gameObject.GetComponent<PlayerMovement>().canMove = true;
    }

    IEnumerator CollectablePopUp()
    {
        //player.gameObject.GetComponent<PlayerMovement>().canMove = false;
        // wait for waitime, then teleport player
        this.transform.Find("PopUp").gameObject.SetActive(true);
        gnome.GetComponent<PlayerMovement>().canMove = false;
        yield return new WaitForSeconds(3.5f);
        gnome.GetComponent<PlayerMovement>().canMove = true;
        this.transform.Find("PopUp").gameObject.SetActive(false);
    }

    public void collectablePopUp()
    {
        StartCoroutine("CollectablePopUp");
    }

    //IEnumerator UseItem()
    //{
    //    this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
    //    for (int i = 0; i < 30; i++)
    //    {
    //        if (i == 29)
    //        {
    //            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    //        }
    //        yield return null;
    //    }
    //}
}
