using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager :Sigleton<AudioManager>
{
    public List<AudioType> audioTypes;
    public Dictionary<string,AudioType> audioTag = new Dictionary<string, AudioType>();




    protected override void Awake()
    {
        base.Awake();
        //DontDestroyOnLoad(gameObject);
    }
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        foreach(var type in audioTypes){
            type.soruce = gameObject.AddComponent<AudioSource>();
            type.soruce.clip = type.clip;
            type.soruce.name = type.name;
            type.soruce.volume = type.volume;
            type.soruce.pitch = type.pitch;
            type.soruce.loop = type.isLoop;
            audioTag.Add(type.name,type);
        }
    }

    public void Play(string name){
        if(audioTag.ContainsKey(name)){
            audioTag[name].soruce.Play();
        }else{
            Debug.LogWarning("音频未注册");
        }
    }

    public void Pause(string name){
        if(audioTag.ContainsKey(name)){
            audioTag[name].soruce.Pause();
        }else{
            Debug.LogWarning("音频未注册");
        }
    }
    public void Stop(string name){
        if(audioTag.ContainsKey(name)){
            audioTag[name].soruce.Stop();
        }else{
            Debug.LogWarning("音频未注册");
        }
    }

    public bool IsPlay(string name){
        if(audioTag.ContainsKey(name)){
            return audioTag[name].soruce.isPlaying;
        }else{
            Debug.LogWarning("音频未注册");
            return false;
        }
    }

    public void SetPitch(string name,float pitch){
        if(audioTag.ContainsKey(name)){
            audioTag[name].soruce.pitch = pitch;
            return;
        }else{
            Debug.LogWarning("音频未注册");
            return;
        }
    }
    public float GetPitch(string name){
        if(audioTag.ContainsKey(name)){
            return audioTag[name].soruce.pitch;
        }else{
            Debug.LogWarning("音频未注册");
            return 0;
        }
    }


}
