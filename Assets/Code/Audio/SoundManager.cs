using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    // Outlets
    AudioSource audioSource;
    public AudioClip coinCollectSound;
    public AudioClip bowlBreakSound;
    public AudioClip jumpSound;
    public AudioClip doubleJumpTokenCollectSound;
    public AudioClip buttonClickSound;
    public AudioClip lossSound;
    public AudioClip winSound;
    
    float maxVolume = 1;
    void Awake()
    {
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

    public void PlayCoinCollectSound() {
        audioSource.volume = maxVolume * 0.25f;
        audioSource.PlayOneShot(coinCollectSound);
    }
    public void PlayBowlBreakSound() {
        audioSource.volume = maxVolume * 1f;
        audioSource.PlayOneShot(bowlBreakSound);
    }
    public void PlayJumpSound() {
        audioSource.volume = maxVolume * 0.1f;
        audioSource.PlayOneShot(jumpSound);
    }
    public void PlayDoubleJumpTokenCollectSound() {
        audioSource.volume = maxVolume * 0.25f;
        audioSource.PlayOneShot(doubleJumpTokenCollectSound);
    }
    public void PlayButtonClickSound() {
        audioSource.volume = maxVolume * 0.1f;
        audioSource.PlayOneShot(buttonClickSound);
    }
    public void PlayLossSound() {
        audioSource.volume = maxVolume * 0.1f;
        audioSource.PlayOneShot(lossSound);
    }
    public void PlayWinSound() {
        audioSource.volume = maxVolume * 0.1f;
        audioSource.PlayOneShot(winSound);
    }
}
