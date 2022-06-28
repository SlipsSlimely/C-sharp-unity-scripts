using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnJewels : MonoBehaviour
{
    public int xRange = 10;
    public int yRange = 3;
    public int numObjects = 16;

    public GameObject[] objects;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        for (int i = 0; i <= numObjects; i++)
        {
            Vector3 spawnLoc = new Vector3(Random.Range(-xRange, xRange), Random.Range(-yRange, yRange), 0);
            int objectPick = Random.Range(1, objects.Length);
            Instantiate(objects[objectPick], spawnLoc, Random.rotation);
        }
    }
}
