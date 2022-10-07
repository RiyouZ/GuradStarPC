using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : Sigleton<GameObjectPool>
{
    public List<string> gameObjectName;
    public List<GameObject> gameObjectList;


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
        if(poolName.ContainsKey(name)){
            Debug.Log("游戏对象未注册");
            return null;
        }
        return poolName[name];
    }
    protected virtual string GetName(GameObject obj){
        return poolObject[obj];
    }

    public virtual GameObject Pop(string name){
        GameObject tmp = null;
        if(pool.ContainsKey(name)){
            if(pool[name].Count>0){
                tmp = pool[name].Dequeue();
                tmp.SetActive(true);
            }else{
                tmp = Instantiate(GetObject(name),this.transform);
                pool[name].Enqueue(tmp);
            }
            return tmp;
        }else{
            Debug.Log("未在池内找到对象");
            return null;
        }
    }

    public virtual void Push(GameObject item){
        if(!poolObject.ContainsKey(item))return;
        if(pool[GetName(item)].Count<=maxCnt&&pool[GetName(item)].Contains(item)){
            item.SetActive(false);
            pool[GetName(item)].Enqueue(item);
        }else{
            Destroy(item);
        }
    }

    protected virtual void InitPool(){
        InitList();
        GameObject tmp;
        foreach(var item in gameObjectList){
            for(int i = 1;i<=maxCnt;i++){
                tmp = Instantiate(item,this.transform);
                pool[GetName(item)].Enqueue(tmp);
                tmp.SetActive(false);
            }
        }

    }

    protected virtual void InitList(){
        for(int i = 0;i<gameObjectName.Count;i++){
            AddNameWithObject(gameObjectName[i],gameObjectList[i]);
            pool.Add(gameObjectName[i],new Queue<GameObject>());
        }
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
