using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatsScript : MonoBehaviour
{
    public TMPro.TMP_InputField cheat;
    public TMPro.TMP_Text feedback;

    void Start()
    {
        feedback.text = "";
        cheat = gameObject.GetComponent<TMPro.TMP_InputField>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            CheckCheat();
        }
    }

    public void CheckCheat()
    {
        if(cheat.text == "god")
        {
            GameStates.isInvincible = true;
            feedback.text = "God Mode Enabled";

        }
        if (cheat.text == "greedy")
        {
            GameStates.isGreedy = true;
            feedback.text = "Greedy Mode Enabled";


        }
    }
}
