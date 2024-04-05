using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbsContainer : MonoBehaviour
{
    public List<GameObject> orbsList = new();
    GameObject greenOrb;
    GameObject redOrb;
    GameObject blueOrb;

    // Start is called before the first frame update
    void Start()
    {
        orbsList.Add(greenOrb);
        orbsList.Add(redOrb);
        orbsList.Add(blueOrb);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
