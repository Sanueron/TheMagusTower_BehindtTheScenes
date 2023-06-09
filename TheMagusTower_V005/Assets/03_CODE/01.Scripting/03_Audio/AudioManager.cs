using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    AudioMixer audioMixer;
    [SerializeField] private int musicOnOff, sfxOnOff;

    [SerializeField]
    Slider musicSlider, sfxSlider;
    // Start is called before the first frame update
    void Start()
    {
        musicOnOff = 0;
        sfxOnOff = 0;
    }

    #region Music and SFX On/Off and volume control
    public void MusicOnOff()
    {
        if (musicOnOff == 0)
        {
            audioMixer.SetFloat("MusicVolume", -80);
            musicOnOff = 1;
        }
        else
        {
            audioMixer.SetFloat("MusicVolume", 0);
            musicOnOff = 0;
        }
    }
    public void SFXOnOff()
    {
        if (sfxOnOff == 0)
        {
            audioMixer.SetFloat("SFXVolume", -80);
            sfxOnOff = 1;
        }
        else
        {
            audioMixer.SetFloat("SFXVolume", 0);
            sfxOnOff = 0;
        }
    }
    public void ManageMusicVolume()
    {
        audioMixer.SetFloat("MusicVolume", musicSlider.value);
    }
    public void ManageSFXVolume()
    {
        audioMixer.SetFloat("SFXVolume", sfxSlider.value);
    }
    #endregion
}
