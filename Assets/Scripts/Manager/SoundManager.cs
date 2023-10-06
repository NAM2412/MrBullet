using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private AudioSource audioSource;

    void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }
    
    public void PlaySoundEffect(AudioClip clip, float volume)
    {
        audioSource.PlayOneShot(clip, volume);
    }
}
