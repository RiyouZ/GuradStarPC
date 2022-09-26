using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData",menuName = "Data/CharacterData",order = 1)]
public class CharacterSO : ScriptableObject
{
    [Header("速度")]
    public float curSpeed;
    public float maxSpeed;
    public float accSpeed;
    [Header("生命")]
    public int curHealth;
    public int maxHealth;
    [Header("武器")]
    public float hotTime;
    public float curTime;
    public float coolTime;
    [Header("油量")]
    public float curOil;
    public float maxOil;
    [Header("操作状态")]
    public bool isGrab;
    public bool isUnpower;
    public bool isDead;

}
