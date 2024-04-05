using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlingSoundsSystems : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] soundSystem = GameObject.FindGameObjectsWithTag("SoundSystem");
        if(soundSystem.Length > 1)
        {
            Destroy(soundSystem[1]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
