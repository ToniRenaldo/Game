using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource bgmAudioSource;
    [Header("ButtonBGM")]
    public GameObject buttonBGM;
    public GameObject imageMuteBGM;
    public GameObject imageUnMuteBGM;

    [Header("Clips")]
    public AudioClip battleClip;
    public AudioClip roamingClip;
    public float defaultVolume;

    [Header("SFX")]

    public AudioSource sfxAudioSource;
    [Header("Button SFx")]
    public GameObject buttonSfx;
    public GameObject imageMuteSfx;
    public GameObject imageUnMuteSfx;

    private void Start()
    {
        buttonBGM.GetComponent<Button>().onClick.AddListener(ToggleBGM);
        buttonSfx.GetComponent<Button>().onClick.AddListener(ToggleSFX);    
    }
    public void ToggleBGM()
    {
        if(bgmAudioSource.volume  != 0)
        {
            bgmAudioSource.volume = 0;
            
            imageMuteBGM.SetActive(true);
            imageUnMuteBGM.SetActive(false);
        }
        else
        {
            bgmAudioSource.volume = defaultVolume;
            imageMuteBGM.SetActive(false);
            imageUnMuteBGM.SetActive(true);
        }
    }

    public void ToggleSFX()
    {
        if (sfxAudioSource.volume != 0)
        {
            sfxAudioSource.volume = 0;
            imageMuteSfx.SetActive(true);
            imageUnMuteSfx.SetActive(false);
        }
        else
        {
            sfxAudioSource.volume = defaultVolume;
            imageMuteSfx.SetActive(false);
            imageUnMuteSfx.SetActive(true);
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
