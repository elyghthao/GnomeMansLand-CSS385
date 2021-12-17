using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpikeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject leftMostSpawn;
    [SerializeField] private GameObject rightMostSpawn;
    [SerializeField] private GameObject playerNewStart; //Position player should move to
    private float spawnPosY;
    private float xMin, xMax;
    [SerializeField] private float spawnRate = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosY = leftMostSpawn.transform.position.y;
        xMin = leftMostSpawn.transform.position.x;
        xMax = rightMostSpawn.transform.position.x;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            StartCoroutine("SpawnSpikes");

        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            StopCoroutine("SpawnSpikes");

        }
    }

    IEnumerator SpawnSpikes()
    {
        float spawnPosX;
        while (true)
        {
            spawnPosX = Random.Range(xMin, xMax);
            GameObject spike = Instantiate(Resources.Load("Prefabs/Obstacles/FallingSpike") as GameObject);
            spike.transform.localPosition = new Vector3(spawnPosX, spawnPosY, 0);
            spike.GetComponent<PortalBehavior>().playerNewStart = playerNewStart;
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
