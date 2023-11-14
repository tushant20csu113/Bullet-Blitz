using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}
public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private Sound[] musicSounds, sfxSounds;
    [SerializeField]
    private AudioSource musicSource, sfxSource;
    [HideInInspector]
    public float tempSfx=1f, tempMusic=1f;

    public static AudioManager Instance { get; private set; }
    private void Start()
    {
        PlayMusic("Theme");
    }
    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        LevelTracker.OnStateChange += OnStateChange;
    }



    private void OnDisable()
    {
       LevelTracker.OnStateChange -= OnStateChange;
    }
    private void OnStateChange(LEVEL_STATE currentlevelState)
    {
        switch (currentlevelState)
        {
            case LEVEL_STATE.COMPLETED:
                {
                   /* ToggleMusic();
                    ToggleSFX();*/
                }
                break;
            case LEVEL_STATE.GAME_OVER:
                {
                   /* ToggleMusic();
                    ToggleSFX();*/
                }
                break;
        }
    }


    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);
        if(s==null)
        {
            Debug.Log("Sound not found");
        }
        else

        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }
    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else

        {
            sfxSource.PlayOneShot(s.clip);
        }
    }
    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }
    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }
    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
        tempMusic = volume;
    }
    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
       tempSfx = volume;
    }
}
