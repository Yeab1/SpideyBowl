using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyAudio : MonoBehaviour
{
    public static DontDestroyAudio instance;
    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void updateMusicVolume(float value) {
        audioSource.volume = value;
    }
    public float getVolume() {
        return audioSource.volume;
    }
}
