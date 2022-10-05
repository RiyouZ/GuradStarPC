using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SuppliesData", menuName = "Data/SuppliesData",order = 1)]
public class SuppliesSO : ScriptableObject
{

    [Header("属性")]
    public float recoverValue;
    public float curLifeTime;
    public float maxLifeTime;

    
}
