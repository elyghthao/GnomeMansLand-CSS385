/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    private Transform targetA, targetB;
    [SerializeField] private float distance;
    [SerializeField] private bool upDown;
    [SerializeField] private float speed = 1f; //Change this to suit your game.
    private bool switching = false;

    private void Start()
    {
        targetA = transform;
        targetB = targetA;
        if (upDown)
        {
            Vector3 newB = targetA.position;
            newB.y += distance;
            targetB.position = newB;
        }
        else
        {
            Vector3 newB = targetA.position;
            newB.x += distance;
            targetB.localPosition = newB;
        }
    }

    void Update()
    {
        if (!switching)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetB.localPosition, speed * Time.deltaTime);
        }
        else if (switching)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetB.position, speed * Time.deltaTime);
        }
        if (transform.position.x >= targetB.position.x || transform.position.y >= targetB.position.y)
        {
            switching = true;
        }
        else if (transform.position.x <= targetB.position.x || transform.position.y <= targetB.position.y)
        {
            switching = false;
        }
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private float distance;
    [SerializeField] private Transform endpos;
    [SerializeField] private bool goUpFirst;
    private Vector2 startPos;

    // Start is called before the first frame update
    void Start()
    {
        if (goUpFirst)
        {
            Vector2 temp = transform.position;
            temp.y += distance;
            startPos = temp;
        }
        else
        {
            Vector2 temp = transform.position;
            temp.y -= distance;
            startPos = temp;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (goUpFirst)
        {
            transform.Translate(0, speed * 1f * Time.deltaTime, 0);
        }
        else
        {
            transform.Translate(0, speed * -1f * Time.deltaTime, 0);
        }

        if (goUpFirst)
        {
            if (transform.position.y > (startPos.y + distance))
            {
                goUpFirst = !goUpFirst;
            }
        }
        else
        {
            if (transform.position.y < (startPos.y - distance))
            {
                goUpFirst = !goUpFirst;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !goUpFirst)
        {
            goUpFirst = !goUpFirst;
        }
    }
}
