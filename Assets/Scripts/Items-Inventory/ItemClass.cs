using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Item 
{
    public enum ItemType
    {
        Spade, 
        WateringCan,
        Hammer,
        Key, 
        // COLLECTABLE
        Collectable
        // COLLECTABLE
    }

    [SerializeField]
    public ItemType itemType;

    public bool pickedup = false;
    public bool addCol = true;
    public int index;

    // COLLECTABLE
    [SerializeField] Sprite collectableSprite;
    public bool collectable = false;
    // COLLECTABLE
    
    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Spade: return ItemAssets.Instance.SpadeSprite;
            case ItemType.WateringCan: return ItemAssets.Instance.WateringCanSprite;
            case ItemType.Hammer: return ItemAssets.Instance.HammerSprite;
            case ItemType.Key: return ItemAssets.Instance.KeySprite;
            // COLLECTABLE
            case ItemType.Collectable: return collectableSprite;
            // COLLECTABLE
        }
    }

    public void pickUp()
    {
        pickedup = true;
        this.GetComponent<BoxCollider2D>().enabled = false;
        StopUsing();
    }
}
