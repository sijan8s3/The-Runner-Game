using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour
{
    [SerializeField] Sprite musicOn;
    [SerializeField] Sprite musicOff;
    [SerializeField] Button button;
    public static bool isMute = false;
    [SerializeField] GameObject soundSystem;

    void Start()
    {
        button.onClick.AddListener(ToggleButton);
        soundSystem = GameObject.FindWithTag("SoundSystem");
        gameObject.GetComponent<Image>().sprite = !isMute ? musicOff : musicOn;

    }

    void ToggleButton()
    {
        isMute = !isMute;
        soundSystem.transform.GetChild(0).GetComponent<AudioSource>().enabled = !isMute;
        gameObject.GetComponent<Image>().sprite = !isMute ? musicOff : musicOn;
    }
}
