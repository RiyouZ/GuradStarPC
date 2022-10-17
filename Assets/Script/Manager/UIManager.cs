using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // //暂停菜单打开
    // public void StopMenuOpenAction(){
    //     GameManager.Instance.GamePasue();
    // }
    // //暂停菜单关闭
    // public void StopMenuCloseAction(){
    //     GameManager.Instance.GameStart();
    // }
    


}
