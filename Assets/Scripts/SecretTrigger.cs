using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretTrigger : MonoBehaviour
{
    [SerializeField] GameObject parentGrid;

    [SerializeField] GameObject secretLayer;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            // Hide secret layer
            Transform secretLayer = parentGrid.transform.Find("SecretLayer");
            secretLayer.gameObject.SetActive(false);
        }
    }*/

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            //Debug.Log("TRIGGERED");
            // Hide secret layer
            
            //if (secretLayer.gameObject.activeSelf)
            //{
                secretLayer.SetActive(false);
            //}
            //else
            //{
                //secretLayer.gameObject.SetActive(true);
            //}
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            secretLayer.SetActive(true);
        }
    }

}
