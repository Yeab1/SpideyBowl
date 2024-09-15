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
    public AudioClip slipSound;

    float maxVolume = 1f; // Global volume setting

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

    public void updateSFXVolume(float value)
    {
        maxVolume = value;
    }

    // Adjust the volume of the sound based on the global volume setting
    float AdjustedVolume(float relativeVolume)
    {
        return maxVolume * relativeVolume;
    }

    public void PlayCoinCollectSound()
    {
        audioSource.volume = AdjustedVolume(0.2f);
        audioSource.PlayOneShot(coinCollectSound);
    }

    public void PlayBowlBreakSound()
    {
        audioSource.volume = AdjustedVolume(1f);
        audioSource.PlayOneShot(bowlBreakSound);
    }

    public void PlayJumpSound()
    {
        audioSource.volume = AdjustedVolume(0.1f);
        audioSource.PlayOneShot(jumpSound);
    }

    public void PlayDoubleJumpTokenCollectSound()
    {
        audioSource.volume = AdjustedVolume(0.25f);
        audioSource.PlayOneShot(doubleJumpTokenCollectSound);
    }

    public void PlayButtonClickSound()
    {
        audioSource.volume = AdjustedVolume(0.2f);
        audioSource.PlayOneShot(buttonClickSound);
    }

    public void PlayLossSound()
    {
        audioSource.volume = AdjustedVolume(0.1f);
        audioSource.PlayOneShot(lossSound);
    }

    public void PlayWinSound()
    {
        audioSource.volume = AdjustedVolume(0.1f);
        audioSource.PlayOneShot(winSound);
    }

    public void PlaySlipSound()
    {
        audioSource.volume = AdjustedVolume(0.25f);
        audioSource.PlayOneShot(slipSound);
    }

    public float getVolume()
    {
        return maxVolume;
    }
}
