using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource bgmAudioSource;
    public AudioClip battleClip;
    public AudioClip roamingClip;
    public float defaultVolume;

    public AudioSource sfxAudioSource;
    public GameObject tempAudioSource; 
    public void ToggleBGM()
    {
        if(bgmAudioSource.volume  != 0)
        {
            bgmAudioSource.volume = 0;
        }
        else
        {
            bgmAudioSource.volume = defaultVolume;
        }
    }
    
    public void SetBattleBGM()
    {
        bgmAudioSource.Stop();
        bgmAudioSource.clip = battleClip;
        bgmAudioSource.Play();
    }
    public void SetRoamingBGM()
    {
        bgmAudioSource.Stop();
        bgmAudioSource.clip = roamingClip;
        bgmAudioSource.Play();
    }

}
