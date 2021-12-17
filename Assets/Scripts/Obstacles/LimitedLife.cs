using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedLife : MonoBehaviour
{
    [SerializeField] private float lifeDuration = 5f; //in seconds
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ObjectAlive");
    }

    IEnumerator ObjectAlive()
    {
        yield return new WaitForSeconds(lifeDuration);
        Destroy(this.gameObject);
    }
}
