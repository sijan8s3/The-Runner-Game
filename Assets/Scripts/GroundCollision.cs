using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GroundCollision : MonoBehaviour
{
    public GroundSpawner groundSpawner;
    public List<GameObject> tileObjects;
    public OrbsContainer orbsContainer;
    private List<int> busyLanes = new();
    public GameObject Obstacle;

    // Start is called before the first frame update
    void Start() 
    {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
        orbsContainer = GameObject.FindObjectOfType<OrbsContainer>();
        SpawnObjects();

    }

    void SpawnObjects()
    {
        List<GameObject> spawnedObjects = new();
        //Generate number of objects to be spawned randomly
        int numberOfObjects = Random.Range(0, 4);
        //generate a variable to track how many obstacles have been spawned
        int obstaclesSoFar = 0;

        for (int i = 0; i < numberOfObjects; i++)
        {
            //decides to make an orb or an obstacle randomly where 0 is an orb and 1 is an obstacle
            int typeOfObject = Random.Range(0, 2);
            //chooses the object position randomly
            int placeOfObject = Random.Range(2, 5);

            if (typeOfObject == 1 && obstaclesSoFar <2)
            {
                if (!busyLanes.Contains(placeOfObject))
                {
                    Transform spawnPoint = gameObject.transform.GetChild(placeOfObject).transform;
                    //spawnPoint.position += nextSpawnPoint;
                    GameObject temp = Instantiate(Obstacle, spawnPoint.position, Quaternion.identity);
                    spawnedObjects.Add(temp);
                    busyLanes.Add(placeOfObject);
                    obstaclesSoFar++;
                }
                else
                {
                    i--;
                    continue;
                }
            } else
            {
                //chooses the orb color randomly
                int typeOfOrb = Random.Range(0, 3);
                //checks if the lane as busy
                if (!busyLanes.Contains(placeOfObject))
                {
                    Transform spawnPoint = gameObject.transform.GetChild(placeOfObject).transform;
                    //spawnPoint.position += nextSpawnPoint;
                    GameObject temp = Instantiate(orbsContainer.orbsList[typeOfOrb], spawnPoint.position, Quaternion.identity);
                    spawnedObjects.Add(temp);
                    busyLanes.Add(placeOfObject);
                }
                else
                {
                    i--;
                    continue;
                }

            }

        }
        tileObjects = spawnedObjects;
        //foreach(int bla in busyLanes)
        //{

        //    Debug.Log(bla);
        //}
    }

    private void OnTriggerExit(Collider other)
    {
       groundSpawner.SpawnTile();
       foreach(GameObject tileObject in tileObjects)
       {
          Destroy(tileObject, 2);
       }
        Destroy(gameObject, 2);
    }

}
