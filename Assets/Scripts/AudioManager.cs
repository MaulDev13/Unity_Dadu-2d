using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Transform audioParent;
    [SerializeField] private GameObject soundBoxPrefab;

    public Sound[] sounds;

    public static AudioManager instance;

    private void Awake()
    {
        Singleton();
        InitSound();
    }

    private void Start()
    {
        //Play("Theme");
    }

    private void Singleton()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(this);
    }

    private void InitSound()
    {
        foreach (Sound s in sounds)
        {
            s.source = new AudioSource[s.clips.Length];
            int i = 0;

            foreach(AudioClip ac in s.clips)
            {
                s.source[i] = audioParent.gameObject.AddComponent<AudioSource>();

                s.source[i].clip = ac;

                s.source[i].volume = s.volume;
                s.source[i].pitch = s.pitch;
                s.source[i].loop = s.loop;

                s.source[i].spatialBlend = s.spatialBlend;
                s.source[i].minDistance = s.minDistance;
                s.source[i].maxDistance = s.maxDistance;
                i++;
            }
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning($"Sound {name} not found!");
            return;
        } 

        if(s.clips.Length < 0)
        {
            Debug.LogWarning($"Sound {name} don't have any clip!");
            return;
        } else
        {
            int index = UnityEngine.Random.Range(0, s.clips.Length);
            s.source[index].Play();
        }
    }

    public void Play(string name, Vector2 post)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning($"Sound {name} not found!");
            return;
        }

        if (s.clips.Length < 0)
        {
            Debug.LogWarning($"Sound {name} don't have any clip!");
            return;
        }
        else
        {
            int index = UnityEngine.Random.Range(0, s.clips.Length);

            var soundBox = Instantiate(soundBoxPrefab, post, Quaternion.identity) as GameObject;
            var audioSource = soundBox.GetComponent<AudioSource>() as AudioSource;
            audioSource.clip = s.source[index].clip;
            audioSource.volume = s.source[index].volume;
            audioSource.pitch = s.source[index].pitch;
            audioSource.loop = s.source[index].loop;
            audioSource.spatialBlend = s.source[index].spatialBlend;
            audioSource.minDistance = s.source[index].minDistance;
            audioSource.maxDistance = s.source[index].maxDistance;

            audioSource.Play();

            s.source[index].Play();

            Destroy(soundBox, 1f);
        }
    }
}
