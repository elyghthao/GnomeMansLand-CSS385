using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBehavior : MonoBehaviour
{
    [SerializeField] private FoodSpawner.FoodType foodType;
    public FoodSpawner spawner;
    [SerializeField] private GameObject wingLeft, wingRight;
    private GameObject player;

    private void Start()
    {
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == spawner.player)
        {
            


            spawner.setWings(wingLeft, wingRight);
            spawner.applyEffect(foodType);
        }
        spawner.playSound();
        Destroy(transform.gameObject);  // kills self
    }
}
