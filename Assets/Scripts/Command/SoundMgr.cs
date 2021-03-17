using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundMgr : MonoBehaviour
{
    public static SoundMgr Instance;
    private Dictionary<string, AudioClip> dictSound = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioSource> dictSource = new Dictionary<string, AudioSource>();
    public AudioClip[] gameSounds;
    public AudioClip[] soundRandomSystem;
    public AudioSource mainCamera;
    public AudioSource tailParticle;
    public AudioSource alarmSound;
    public AudioSource UICamera;
    public AudioSource terrain;
    public AudioSource player;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start ()
    {
        find();
	}
    public void play(string audioSourceName,string musicName)
    {
        AudioClip audioClip = dictSound[musicName];
        AudioSource audioSource = dictSource[audioSourceName];
        audioSource.Stop();
        audioSource.PlayOneShot(audioClip);
    }
    public void stop(string audioSourceName)
    {
        AudioSource audioSource = dictSource[audioSourceName];
        audioSource.Stop();
    }
    private void find()
    {
        for (int i=0;i<gameSounds.Length;i++)
        {
            dictSound.Add(gameSounds[i].name,gameSounds[i]);
        }
        for (int i=0;i<soundRandomSystem.Length;i++)
        {
            dictSound.Add(soundRandomSystem[i].name,soundRandomSystem[i]);
        }
        dictSource.Add("mainCamera",mainCamera);
        dictSource.Add("tailParticle",tailParticle);
        dictSource.Add("alarmSound", alarmSound);
        dictSource.Add("uiCamera",UICamera);
        dictSource.Add("terrain", terrain);
        dictSource.Add("player",player);
    }
    public void setAudioValue(float value)
    {
        foreach (AudioSource audioSource in dictSource.Values)
        {
            audioSource.volume = value;
        }
    }
}
