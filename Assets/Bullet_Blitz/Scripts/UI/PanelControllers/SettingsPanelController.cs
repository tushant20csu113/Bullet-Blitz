using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingsPanelController : MonoBehaviour
{
    
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    private void OnEnable()
    {
        musicSlider.value = AudioManager.Instance.tempMusic;
        sfxSlider.value = AudioManager.Instance.tempSfx;
    }
    private void OnDisable()
    {

        AudioManager.Instance.tempMusic = musicSlider.value;
        AudioManager.Instance.tempSfx =sfxSlider.value ;
    }
    public void MusicSlider()
    {
        AudioManager.Instance.MusicVolume(musicSlider.value);
    }
    public void SFXSlider()
    {
        AudioManager.Instance.SFXVolume(sfxSlider.value);
    }
}
