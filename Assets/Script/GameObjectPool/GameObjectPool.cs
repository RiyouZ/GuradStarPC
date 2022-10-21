using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : Sigleton<GameObjectPool>
{
    public List<string> gameObjectName;
    public List<GameObject> gameObjectList = new List<GameObject>();


    public Dictionary<string,Queue<GameObject>> pool = new Dictionary<string, Queue<GameObject>>();
    public  Dictionary<string,GameObject> poolName = new Dictionary<string, GameObject>();
    public  Dictionary<GameObject,string> poolObject = new Dictionary<GameObject,string>();

    // public Queue<GameObject> oilPool = new Queue<GameObject>();
    // public Queue<GameObject> healthPool = new Queue<GameObject>();
    public int defaultCnt;
    public int maxCnt;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        InitPool();
        //PoolDebug();
    }

    protected virtual GameObject GetObject(string name){
        if(!poolName.ContainsKey(name)){
            Debug.Log("游戏对象未注册");
            return null;
        }
        return poolName[name];
    }
    protected virtual string GetName(GameObject obj){
        return poolObject[obj];
    }

    public virtual GameObject Pop(GameObject item){
        GameObject tmp = null;
        if(pool.ContainsKey(item.name)){
            if(pool[item.name].Count>0){
                tmp = pool[item.name].Dequeue();
            }
        }else{
            tmp = Instantiate(item,this.transform);
            tmp.name = item.name;
            pool.Add(tmp.name,new Queue<GameObject>());
            pool[tmp.name].Enqueue(item);
            //Debug.Log("未在池内找到对象");
        }
        tmp.SetActive(true);
        return tmp;
    }

    public virtual void Push(GameObject item){
        if(pool.ContainsKey(item.name)){
            if(pool[item.name].Count<=maxCnt){
                //Debug.LogWarning("池内有此对象");
                pool[item.name].Enqueue(item);
                item.SetActive(false);
                return;
            }
        }else if(!pool.ContainsKey(item.name)){
            //Debug.LogWarning("池内没有此对象");
            GameObject tmp = new GameObject();
            tmp = Instantiate(tmp,this.transform);
            tmp.name = item.name;
            pool.Add(tmp.name,new Queue<GameObject>());    
            pool[tmp.name].Enqueue(tmp);
            tmp.SetActive(false);
            //Debug.LogWarning("已加入池内");
            return;
        }else{
            Destroy(item);
        }
    }

    protected virtual void InitPool(){
        // InitList();
        foreach(var item in gameObjectList){
            GameObject tmp = new GameObject();
            for(int i = 1;i<=maxCnt;i++){
                tmp = Instantiate(item,this.transform);
                tmp.name = item.name;
                if(!pool.ContainsKey(item.name)){
                    pool.Add(tmp.name,new Queue<GameObject>());
                    pool[tmp.name].Enqueue(tmp);
                }else{
                    pool[tmp.name].Enqueue(tmp);
                }
                tmp.SetActive(false);
            }
        }

    }

    protected virtual void InitList(){
        // for(int i = 0;i<gameObjectName.Count;i++){
        //     AddNameWithObject(gameObjectName[i],gameObjectList[i]);
        // }
    }

    protected virtual void AddNameWithObject(string name,GameObject obj){
        poolName.Add(name,obj);
        poolObject.Add(obj,name);
    }

    public void PoolDebug(){
        foreach(var item in gameObjectName){
            Debug.Log(item+"+"+pool[item].Dequeue());
        }


    }
}
