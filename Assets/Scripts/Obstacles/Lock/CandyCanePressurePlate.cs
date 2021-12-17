using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyCanePressurePlate : MonoBehaviour
{
    [SerializeField] private CandyCaneDoor door;
    private float timer;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
                door.Close();
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Pressure plate activated");
        door.Open();
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        timer = 1f;
    }
}
