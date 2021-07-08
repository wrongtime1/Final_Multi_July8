﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    void Awake()
    {
        if (instance != null){
            //Destroy(gameObject);
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            instance = this;
        }
        foreach (Sound s in sounds)
        {
          s.source=  gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }

    }

 public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    
            }
}
