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
    public void initialize_volume(float volume) {
        audioSource.volume = volume;
    }
    public void updateMusicVolume(float value) {
        audioSource.volume = value;
        SoundManager.save_bg_volume(value);
    }
    public float getVolume() {
        return audioSource.volume;
    }
}
