using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


[System.Serializable]
public class AudioType
{
    //组件
    public AudioSource soruce;
    public AudioClip clip;

    //属性
    public string name;
    [Range(0,1)]
    public float volume;
    [Range(0,2)]
    public float pitch;
    //状态
    public bool isLoop;


}
