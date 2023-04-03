using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public Sound[] themes;

    public List<Sound> enabledThemes;

    public static AudioManager Instance { get; private set; }
    

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            // Set the static instance to this instance
            Instance = this;
        }

        foreach(Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
        
        foreach(Sound theme in themes)
        {
            theme.source = gameObject.AddComponent<AudioSource>();
            theme.source.clip = theme.clip;

            theme.source.volume = theme.volume;
            theme.source.pitch = theme.pitch;
            theme.source.loop = theme.loop;
        }
    }

    private void Start() {
        removeDisableTheme();
        RandomPlayTheme();
    }

    public void PlaySound(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        sound.source.Play();
    }

    public void StopSound(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        sound.source.Stop();
    }

    private void RandomPlayTheme()
    {  
        int randomIndex = UnityEngine.Random.Range(0, enabledThemes.Count);
        Sound randomTheme = enabledThemes[randomIndex];

        randomTheme.source.Play();
        // Invoke RandomPlayTheme after the length of the current clip
        Invoke("RandomPlayTheme", randomTheme.source.clip.length);
 
    }

    // Check if any theme enable
    private void removeDisableTheme()
    {
        enabledThemes = new List<Sound>();

        foreach (Sound theme in themes)
        {
            if (theme.enable)
            {
                enabledThemes.Add(theme);
            }
        }

        themes = enabledThemes.ToArray();
    }

}
