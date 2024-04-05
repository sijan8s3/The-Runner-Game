using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundSystem : MonoBehaviour
{
    public AudioClip[] soundEffects ;
    public AudioClip[] backgroundMusic;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlaySFX(Events eventType)
    {
        GameObject.FindGameObjectWithTag("SoundEffects").GetComponent<AudioSource>().clip = soundEffects[(int)eventType];
        GameObject.FindGameObjectWithTag("SoundEffects").GetComponent<AudioSource>().Play();
    }

    public void PlayMusic(Levels level)
    {
        GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().clip = backgroundMusic[(int)level];
        GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().Play();
    }
}
