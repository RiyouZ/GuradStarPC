using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameObjectList",menuName ="Data/GameObjectList",order = 1)]
public class GameObjectListSO : ScriptableObject
{
    [Header("列表对象名")]
    public List<string> ObjectName;
    [Header("列表对象")]
    public List<GameObject> Object;
}
