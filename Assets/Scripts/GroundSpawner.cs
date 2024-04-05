using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField] public GameObject ground;
    [SerializeField] public GameObject obstacle;
    Vector3 nextSpawnPoint;

    public void SpawnTile()
    {
        //create a new instance of the tile
        GameObject temp = Instantiate(ground, nextSpawnPoint, Quaternion.identity);
        //assign the obstacle prefab to the tile
        temp.GetComponent<GroundCollision>().Obstacle = obstacle;
        //update the new spawn point
        nextSpawnPoint = temp.transform.GetChild(1).transform.position;

    }

    


    // Update is called once per frame
    void Start()
    {
        for (int i = 0; i < 9; i++)
        {
            SpawnTile();
        }

    }
}
