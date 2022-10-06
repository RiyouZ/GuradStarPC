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
    }

    protected GameObject GetObject(string name){
        return poolName[name];
    }

    protected string GetName(GameObject obj){
        return poolObject[obj];
    }

    public GameObject pop(string name){
        GameObject tmp;
        if(pool[name].Count>0){
            tmp = pool[name].Dequeue();
            tmp.SetActive(true);
        }else{
            tmp = Instantiate(GetObject(name),this.transform);
        }
        return tmp;
    }

    public void push(GameObject item){
        if(pool[GetName(item)].Count<=maxCnt){
            if(!pool[GetName(item)].Contains(item)){
                item.SetActive(true);
                pool[GetName(item)].Enqueue(item);
            }    
        }else{
            Destroy(item);
        }
    }

    protected void InitPool(){
        InitList();
        GameObject tmp;
        foreach(var item in poolName){
            for(int i = 1;i<=maxCnt;i++){
                tmp = Instantiate(item.Value,this.transform);
                pool[item.Key].Enqueue(tmp);
                tmp.SetActive(false);
            }
        }

    }

    protected void InitList(){
        for(int i = 0;i<gameObjectName.Count;i++){
            AddNameWithObject(gameObjectName[i],gameObjectList[i]);
            pool.Add(gameObjectName[i],new Queue<GameObject>());
        }
    }

    protected void AddNameWithObject(string name,GameObject obj){
        poolName.Add(name,obj);
        poolObject.Add(obj,name);
    }
}
