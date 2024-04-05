using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardForce : MonoBehaviour
{
    public static int movingSpeed;
    // Start is called before the first frame update
    void Start()
    {
        movingSpeed = SetMovingSpeed();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, movingSpeed * Time.fixedDeltaTime);

    }

    public static int SetMovingSpeed()
    {
        int speed = SceneSwitch.difficulty == Difficulty.easy ? 900 :
            SceneSwitch.difficulty == Difficulty.medium ? 1300 :
            SceneSwitch.difficulty == Difficulty.hard ? 1800 : movingSpeed = 0;
        return speed;
    }
}
