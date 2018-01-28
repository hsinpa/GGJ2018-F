using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SoundManager : MonoBehaviour {

    [Serializable]
    public class MusicObject
    {
        public string name;
        public AudioClip clip;
    }

    AudioSource audioSoure;
    public List<MusicObject> musicList = new List<MusicObject>();
    public static SoundManager instance;

    private void Awake()
    {
        instance = this;
        audioSoure = GetComponent<AudioSource>();
     
    }


    public void  plauMusic(string id)
    {
        foreach (MusicObject _mobj in musicList)
        {
            if (id == _mobj.name)
            {
                audioSoure.PlayOneShot(_mobj.clip);

            }

        }

    }
}
