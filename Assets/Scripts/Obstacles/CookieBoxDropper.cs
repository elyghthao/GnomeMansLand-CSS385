using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieBoxDropper : MonoBehaviour
{
    public GameObject cookie;
    AudioSource audio;
    public float shakeDuration = 2f;
    public float decreasePoint = 0.5f;
    bool shaking = false;
    private bool hasFallen = false;
    private Transform popup;


    IEnumerator shakeGameObjectCOR(GameObject objectToShake, float totalShakeDuration, float decreasePoint)
    {
        if (decreasePoint >= totalShakeDuration)
        {
            Debug.LogError("decreasePoint must be less than totalShakeDuration...Exiting");
            yield break; //Exit!
        }

        //Get Original Pos and rot
        Transform objTransform = objectToShake.transform;
        Vector3 defaultPos = objTransform.position;
        Quaternion defaultRot = objTransform.rotation;

        float counter = 0f;

        //Shake Speed
        const float speed = 0.1f;

        //Angle Rotation(Optional)
        const float angleRot = 4;

        //Do the actual shaking
        if (audio != null)
            audio.Play(0);
        while (counter < totalShakeDuration)
        {
            counter += Time.deltaTime;
            float decreaseSpeed = speed;
            float decreaseAngle = angleRot;

            //Shake GameObject
            Vector3 tempPos = defaultPos + UnityEngine.Random.insideUnitSphere * decreaseSpeed;
            tempPos.z = defaultPos.z;
            objTransform.position = tempPos;

            //Only Rotate the Z axis if 2D
            objTransform.rotation = defaultRot * Quaternion.AngleAxis(UnityEngine.Random.Range(-angleRot, angleRot), new Vector3(0f, 0f, 1f));


            yield return null;


            //Check if we have reached the decreasePoint then start decreasing  decreaseSpeed value
            if (counter >= decreasePoint)
            {
                Debug.Log("Decreasing shake");

                //Reset counter to 0 
                counter = 0f;
                while (counter <= decreasePoint)
                {
                    counter += Time.deltaTime;
                    decreaseSpeed = Mathf.Lerp(speed, 0, counter / decreasePoint);
                    decreaseAngle = Mathf.Lerp(angleRot, 0, counter / decreasePoint);

                    Debug.Log("Decrease Value: " + decreaseSpeed);

                    //Shake GameObject
                    Vector3 tempP = defaultPos + UnityEngine.Random.insideUnitSphere * decreaseSpeed;
                    tempP.z = defaultPos.z;
                    objTransform.position = tempP;

                    //Only Rotate the Z axis if 2D
                    objTransform.rotation = defaultRot * Quaternion.AngleAxis(UnityEngine.Random.Range(-decreaseAngle, decreaseAngle), new Vector3(0f, 0f, 1f));

                    yield return null;
                }

                //Break from the outer loop
                break;
            }
        }
        objTransform.position = defaultPos; //Reset to original postion
        objTransform.rotation = defaultRot;//Reset to original rotation
        shaking = false; //So that we can call this function next time

        DropBox();
        Debug.Log("Done!");
    }

    void shakeGameObject(GameObject objectToShake, float shakeDuration, float decreasePoint)
    {
        if (shaking)
        {
            return;
        }
        shaking = true;
        hasFallen = true;
        StartCoroutine(shakeGameObjectCOR(objectToShake, shakeDuration, decreasePoint));
    }

    private void DropBox()
    {
        // Make box fall
        if (cookie.GetComponent<Rigidbody2D>() == null)
        {
            Debug.Log("adding rigidbody");
            cookie.AddComponent<Rigidbody2D>();
            cookie.GetComponent<Rigidbody2D>().mass = 100f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasFallen)
        {
            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("dropping cookie");
                shakeGameObject(cookie, shakeDuration, decreasePoint);
            }
        }
    }

}
