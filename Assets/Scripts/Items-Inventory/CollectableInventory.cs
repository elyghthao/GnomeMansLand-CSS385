using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableInventory : MonoBehaviour
{
    [SerializeField] private UI_Inventory uiInventory;
    private new AudioSource[] audio;

    [SerializeField] private Inventory cInventory;

    private void Awake()
    {
        audio = GetComponents<AudioSource>();
        cInventory = new Inventory();
        if (uiInventory != null)
            uiInventory.SetInventory(cInventory);
    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Item item;
        item = collision.gameObject.GetComponent<Item>();

        if (item != null && !cInventory.getItems().Contains(item))
        {
            audio[0].Play();
            cInventory.addItem(item);
            item.index = cInventory.getItems().Count;
            collision.gameObject.transform.localPosition = this.transform.parent.localPosition;
            //set transparent or inactive
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            collision.gameObject.transform.parent = this.transform.parent;
            // Flips Sprite as needed
            if (this.transform.localScale.x < 0) // if gnome facing left, flip sprite
            {
                collision.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else // if gnome facing right, dont flip
            {
                collision.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }

            Vector3 rot = collision.transform.localScale;

            rot.x *= transform.localScale.x;
            collision.transform.localScale = rot;
        }
    }

}
