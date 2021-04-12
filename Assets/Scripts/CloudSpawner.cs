using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject[] cloudPrefabs;
    public float spawnRangeY = 0.5f ;
    public float spawnPosX = 20;
    public float startDelay = 2;
    public float spawnInterval = 1.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void SpawnRandomAnimal()
    {
        int cloudIndex = Random.Range(0, cloudPrefabs.Length);
        Vector2 spawnPos = new Vector2(spawnPosX + transform.position.x,transform.position.y + Random.Range(0, spawnRangeY));

        Instantiate(cloudPrefabs[cloudIndex], spawnPos, cloudPrefabs[cloudIndex].transform.rotation);
    }
}
