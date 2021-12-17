using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerInventory : MonoBehaviour
{
    
    [SerializeField] private UI_Inventory uiInventory; //need another for collectables? 
    private new AudioSource[] audio;

    [SerializeField] private Inventory inventory;
    // COLLECTABLE 
    [SerializeField] private UI_Inventory cUIInventory;
    [SerializeField] private Inventory cInventory;

    public GameObject player;

    public Canvas cCan;

    private bool addColl = true;

    private void Awake()
    {
        audio = GetComponents<AudioSource>();
        inventory = new Inventory();
        if(uiInventory != null)
            uiInventory.SetInventory(inventory);
        //cInventory = new Inventory();


        //gm = GameManager.instance;

        //CARRY OVER TO NEW SCENE//////////////////////////////////////////////////
        // foreach (Item coll in GameManager.instance.getCollectables())
        // {
        //     cInventory.addItem(coll);
        // }
        ///////////////////////////////////////////////////////////////////////////
    }
    private void Start()
    {
        Debug.Log("Set collectable inventory from playerinventory");
        cInventory = GameManager.instance.cInventory;
        if (cUIInventory != null)
            cUIInventory.SetInventory(cInventory);
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        
        Item item;
        item = collision.gameObject.GetComponent<Item>();
        // COLLECTBALE 
         
        if (item != null)
        {
            // if not a collectable item
            if (!item.collectable)
            {
                if (!inventory.getItems().Contains(item))
                {
                    audio[0].Play();
                    inventory.addItem(item);
                    item.index = inventory.getItems().Count;
                    collision.gameObject.transform.localPosition = this.transform.parent.localPosition;
                    //set transparent or inactive
                    collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    collision.gameObject.transform.parent = this.transform.parent;
                    // Flips Sprite as needed
                    if (this.transform.lossyScale.x < 0) // if gnome facing left, flip sprite
                    {
                        collision.gameObject.GetComponent<SpriteRenderer>().flipX = true;
                        Debug.Log("flip");
                    }
                    else // if gnome facing right, dont flip
                    {
                        collision.gameObject.GetComponent<SpriteRenderer>().flipX = false;
                        Debug.Log("dont flip");
                    }
                    Vector3 rot = collision.transform.localScale;

                    rot.x *= transform.localScale.x;
                    collision.transform.localScale = rot; // DONT NEED THIS? ITEM ROTATION
                }
            }
            else // if is collectable
            {
                foreach (Item coll in cInventory.getItems())
                {
                    if (coll.GetSprite() == item.GetSprite())
                    {
                        item.addCol = false;
                        Debug.Log("REACHED FALSE CHANGE");
                    }
                }
                if (item != null && item.addCol)
                {
                    // Debug.Log("cinventory null: " + cInventory);
                    // GameManager.instance.cInventory.addItem(item);
                    // //cInventory.addItem(item);
                    // item.index = cInventory.getItems().Count;

                    // collision.gameObject.transform.localPosition = this.transform.parent.localPosition;
                    // //set transparent or inactive
                    // collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    // collision.gameObject.transform.parent = this.transform.parent;


                    Debug.Log("cinventory null: " + cInventory);
                    cInventory.addItem(item);
                    item.index = cInventory.getItems().Count;

                    collision.gameObject.transform.localPosition = this.transform.parent.localPosition;
                    //set transparent or inactive
                    collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    collision.gameObject.transform.parent = this.transform.parent;

                    // TO DO COLLECTABLE SCENE

                    if (cInventory.getItems().Count == 12)
                    {
                        StartCoroutine("AllColl");
                    }

                    // /*Vector3 rot = collision.transform.localScale;

                    // rot.x *= transform.localScale.x;
                    // collision.transform.localScale = rot;*/
                }
            }

        }
    }

    IEnumerator AllColl()
    {
        yield return new WaitForSeconds(4);
        cCan.gameObject.SetActive(true);
        this.GetComponentInParent<PlayerMovement>().canMove = false;
        yield return new WaitForSeconds(3.5f);
        this.GetComponentInParent<PlayerMovement>().canMove = true;
        cCan.gameObject.SetActive(false);
    }
}
