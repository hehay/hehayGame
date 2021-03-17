using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundMgr : MonoBehaviour
{
    public static SoundMgr Instance;
    private Dictionary<string, AudioClip> NameAndClip = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioSource> NameAndSource = new Dictionary<string, AudioSource>();
    //public AudioClip[] gameSounds;
    //public AudioClip[] soundRandomSystem;
    public AudioSource[] ArrSource;
    public AudioClip[] ArrClip;
    //public AudioSource mainCamera;
    //public AudioSource tailParticle;
    //public AudioSource alarmSound;
    //public AudioSource UICamera;
    //public AudioSource terrain;
    //public AudioSource player;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start ()
    {
        find();
	}
    public void play(string source,string musicName)
    {
        AudioClip audioClip = NameAndClip[musicName];
        AudioSource audioSource = NameAndSource[source];
        audioSource.Stop();
        audioSource.PlayOneShot(audioClip);
    }
    public void stop(string audioSourceName)
    {
        AudioSource audioSource = NameAndSource[audioSourceName];
        audioSource.Stop();
    }
    private void find()
    {
        //for (int i=0;i<gameSounds.Length;i++)
        //{
        //    NameAndClip.Add(gameSounds[i].name,gameSounds[i]);
        //}
        //for (int i=0;i<soundRandomSystem.Length;i++)
        //{
        //    NameAndClip.Add(soundRandomSystem[i].name,soundRandomSystem[i]);
        //}
        for (int i = 0; i < ArrSource.Length; i++)
        {
            NameAndSource.Add(ArrSource[i].name,ArrSource[i]);
        }
        for (int i = 0; i <ArrClip.Length ; i++)
        {
            NameAndClip.Add(ArrClip[i].name,ArrClip[i]);
        }
        //NameAndSource.Add("mainCamera",mainCamera);
        //NameAndSource.Add("tailParticle",tailParticle);
        //NameAndSource.Add("alarmSound", alarmSound);
        //NameAndSource.Add("uiCamera",UICamera);
        //NameAndSource.Add("terrain", terrain);
        //NameAndSource.Add("player",player);
    }
    public void setAudioValue(float value)
    {
        foreach (AudioSource audioSource in NameAndSource.Values)
        {
            audioSource.volume = value;
        }
    }
}
