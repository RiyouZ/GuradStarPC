using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeManager : Sigleton<ColorChangeManager>
{

    public List<GameObject> itemList = new List<GameObject>();

    public Dictionary<string,MeshRenderer> colorDic =  new Dictionary<string, MeshRenderer>();

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    public void Init(){
        foreach(var it in itemList){
            colorDic.Add(it.name,it.GetComponent<MeshRenderer>());
        }
    }

    public void ChangeColor(string tag,Color color){
        if(!colorDic.ContainsKey(tag))return;
        colorDic[tag].material.color = color;
    }




}
