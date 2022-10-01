using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData",menuName = "Data/CharacterData",order = 1)]
public class CharacterSO : ScriptableObject
{
    [Header("生命")]
    public int curHealth;
    public int maxHealth;
    [Header("武器")]
    public float hotTime;
    public float curTime;
    public float coolTime;
    //攻击力
    public int atkVal;

    [Header("操作状态")]

    public bool isShoot;
    public bool isGrab;
    public bool isDead;

}
