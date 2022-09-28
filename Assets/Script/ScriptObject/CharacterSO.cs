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

    [Header("操作状态")]
    public bool isGrab;
    public bool isDead;

}
