using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SourceType 
{
    Btn,
    Background
}
public class SoundMgr : MonoBehaviour
{
    public static SoundMgr Instance;
    private AudioSource btnSource;
    public AudioSource background;
    private AudioClip[] audioClips;

    private void Awake()
    {
        Instance = this;
        LoadRes();
    }
    void LoadRes()
    {
        btnSource = gameObject.AddComponent<AudioSource>();
        audioClips = Resources.LoadAll<AudioClip>("Music");
        background = transform.Find("BkMusic").GetComponent<AudioSource>();
    }
    public void Play(string name)
    {
        if (!GameMgr.Instance.PlayMusic) return;
        AudioClip clip = FindClipByName(name);
        if (clip == null)
        {
          Debug.Log("没有找到音效" + name);
          return;
        }
        btnSource.Stop();
        btnSource.PlayOneShot(clip);
    }
    public void Play(SourceType type,string name)
    {
        if (!GameMgr.Instance.PlayMusic) return;
        AudioClip clip = FindClipByName(name);
        if (clip == null)
        {
            Debug.Log("没有找到音效" + name);
            return;
        }
        switch (type) 
        {
            case SourceType.Background:
                background.Stop();
                background.clip = clip;
                background.Play();
                background.loop = true;
                break;
        
        }
    }
    public void PlayWithoutCtrl(string name)
    {
        AudioClip clip = FindClipByName(name);
        if (clip == null)
        {
            Debug.Log("没有找到音效" + name);
            return;
        }
        btnSource.Stop();
        btnSource.PlayOneShot(clip);
    }
    private AudioClip FindClipByName(string name)
    {
        foreach (var clip in audioClips)
        {
            if (clip.name == name) return clip;
        }
        return null;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
