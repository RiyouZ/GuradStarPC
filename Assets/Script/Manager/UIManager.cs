using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class UIManager : Sigleton<UIManager>
{
    public List<GameObject> uiList = new List<GameObject>();

    public Dictionary<string,GameObject> uiDic = new Dictionary<string, GameObject>();

    protected override void Awake()
    {
        base.Awake();
    }
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        Init();
        
    }



    //初始化管理UI
    public void Init(){
        foreach(var it in uiList){
            for(int i = 0;i<it.transform.childCount;i++){
                uiDic.Add(it.transform.GetChild(i).name,it.transform.GetChild(i).gameObject);
                if(it.transform.GetChild(i).name=="MainCanvas"||it.transform.GetChild(i).name=="MemberCanvas")continue;
                uiDic[it.transform.GetChild(i).name].SetActive(false);
            }
        }
    }

    //设置启动的UI
    public void OpenUI(string tag){
        if(!uiDic.ContainsKey(tag))return;
        if(uiDic[tag].activeSelf==true)return;
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
        if(uiDic[tag].activeSelf==false)return;
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
        uiDic[tag].transform.Find(textname).GetChild(0).GetComponent<TMP_Text>().text = text;
    }

    //不经过UIManager管理打开
    public void OpenUINotManager(GameObject ui){
        ui.SetActive(true);
    }

    //不经过UIManager管理关闭
    public void CloseUINotManager(GameObject ui){
        ui.SetActive(false);
    }


    //读取场景
    public void UILoadSence(string sence){
        SceneManager.LoadScene(sence);
    }


}
