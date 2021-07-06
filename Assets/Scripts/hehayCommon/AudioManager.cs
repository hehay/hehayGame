using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource _audioBGM;
    private AudioSource _audioSound;
    private Dictionary<string, AudioClip> _clips;

    void Awake()
    {
        DontDestroyOnLoad(this);
        Instance = this;

        _audioBGM = gameObject.AddComponent<AudioSource>();
        _audioBGM.loop = true;

        _audioSound = gameObject.AddComponent<AudioSource>();

        LoadLocalClip();
        SetBGMVolume(PlayerPrefs.GetFloat("BGM", 1.0f));
        SetSFXVolume(PlayerPrefs.GetFloat("SFX", 1.0f));
    }

    void LoadLocalClip()
    {
        _clips = new Dictionary<string, AudioClip>();
        AudioClip[] clips = Resources.LoadAll<AudioClip>("Audio");
        foreach (var v in clips)
        {
            _clips.Add(v.name, v);
        }
    }

    public void SetBGMVolume(float volume)
    {
        PlayerPrefs.SetFloat("BGM", volume);
        _audioBGM.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat("SFX", volume);
        _audioSound.volume = volume;
    }

    public void PlayBGM(string clipName)
    {
        if (!_clips.ContainsKey(clipName))
        {
            Debug.Log("找不到此音频文件:" + clipName);
            return;
        }
        PlayBGM(_clips[clipName]);
    }

    public void PlayBGM(AudioClip clip)
    {
        if (PlayerPrefs.GetFloat("BGM", 1.0f) == 0.0f)
            return;

        if (clip != null && _audioBGM.clip != clip)
        {
            _audioBGM.clip = clip;
            _audioBGM.Play();
        }
    }

    public void StopBGM()
    {
        _audioBGM.Stop();
    }

    public void PlaySFX(string clipName)
    {
        if (!_clips.ContainsKey(clipName))
        {
            Debug.Log("找不到此音频文件:" + clipName);
            return;
        }
        PlaySFX(_clips[clipName]);
    }

    public void PlaySFX(AudioClip clip)
    {
        if (PlayerPrefs.GetFloat("SFX", 1.0f) == 0.0f)
            return;

        if (clip != null)
        {
            _audioSound.PlayOneShot(clip);
        }
    }

    public void PauseAllListener(bool pause)
    {
        AudioListener.pause = pause;
    }
}
