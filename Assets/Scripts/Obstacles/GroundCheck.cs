using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!grounded)
        {
            if (collision.gameObject.tag == "Player")
            {
                // knock player out
            }
            else
            {
                grounded = true;
                GetComponent<Collider2D>().enabled = false;
            }
        }
    }
}
