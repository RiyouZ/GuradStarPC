using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : Sigleton<UIManager>
{
    public List<GameObject> uiList = new List<GameObject>();

    public Dictionary<string,GameObject> uiDic = new Dictionary<string, GameObject>();

    protected override void Awake()
    {
        base.Awake();
    }



    //初始化管理UI
    public void Init(){
        foreach(var it in uiList){
            uiDic.Add(it.name,it);
        }

    }

    //设置启动的UI
    public void OpenUI(string tag){
        if(!uiDic.ContainsKey(tag))return;
        // if(tag=="MenuCanvas"){
        //     StopMenuOpenAction();
        //     uiDic[tag].SetActive(true);
        // }else if(tag=="WinCanvas"){

        // }else{

        // }
        uiDic[tag].SetActive(true);
    }
    //关闭启动的UI
    public void CloseUI(string tag){
        if(!uiDic.ContainsKey(tag))return;
        // if(tag=="MenuCanvas"){
        //     // StopMenuCloseAction();
        //     uiDic[tag].SetActive(false);
        // }else{
        // }
        uiDic[tag].SetActive(false);
    }

    //设置指定ui文字内容
    public void SetText(string tag,string textname,string text){
        if(!uiDic.ContainsKey(tag))return;
        uiDic[tag].transform.Find(textname).GetComponent<TextMeshPro>().text = text;
    }
    


}
