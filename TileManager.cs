// Jakob Jaeger

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    // An array for storing our tile prefabs in the Tile Manger
    public GameObject[] tilePrefabs;

    // this value is used to decide where tiles spawn. It should be 10 times the size of the tiles X scale
    public float tileLength;

    // Used to move tiles up the Z axis, away from the camera.
    public float tileDistance;

    // the speed that tiles will move under the player, AKA our player speed
    public float tileSpeed;

    // an array conataining all the currently active tiles.
    private List<GameObject> activeTiles = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // Spawn the first Four starting tiles
        GameObject tileObject;
        tileObject = Instantiate(tilePrefabs[0] as GameObject);
        tileObject.transform.SetParent(transform);
        tileObject.transform.position = new Vector3(-tileLength, 0, tileDistance);
        activeTiles.Add(tileObject);

        tileObject = Instantiate(tilePrefabs[0] as GameObject);
        tileObject.transform.SetParent(transform);
        tileObject.transform.position = new Vector3(0, 0, tileDistance);
        activeTiles.Add(tileObject);

        tileObject = Instantiate(tilePrefabs[4] as GameObject);
        tileObject.transform.SetParent(transform);
        tileObject.transform.position = new Vector3(tileLength, 0, tileDistance);
        activeTiles.Add(tileObject);

        SpawnTile();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // When the left most tile goes off screen, destroy it and spawn a new tile.
        if (activeTiles[0].transform.position.x <= (-tileLength*2))
        {
            DeleteTile();
            SpawnTile();
        }

        // This manages the movement of our ground
        for (int i = 0; i < 4; i++)
            activeTiles[i].transform.Translate(Vector3.left * tileSpeed * Time.deltaTime);
    }

    // Method that spawns a new tile on the right side when called
    void SpawnTile(int prefabIndex = -1) 
    {
        GameObject tileObject;

        if(prefabIndex == -1) 
            tileObject = Instantiate(tilePrefabs[RandomPrefabIndex()] as GameObject);
        else
            tileObject = Instantiate(tilePrefabs[prefabIndex] as GameObject);

        tileObject.transform.SetParent(transform);
        tileObject.transform.position = new Vector3(activeTiles[2].transform.position.x + tileLength, 0, tileDistance);
        activeTiles.Add(tileObject);
    }

    // Method that deletes the left most tile and removes it from the list of active tiles
    void DeleteTile() 
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    // Method for randomly selecting a tile from the array of tile prefabs
    int RandomPrefabIndex()
    {
        if (tilePrefabs.Length <= 1)
            return 0;

        int randomIndex = 0;
        randomIndex = Random.Range(1, tilePrefabs.Length);
        return randomIndex;
    }
}
