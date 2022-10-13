using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPool : Sigleton<LaserPool>
{
    public GameObject friendLaser;
    public GameObject enemyLaser;
    public Queue<GameObject> friendPool = new Queue<GameObject>();
    public Queue<GameObject> enemyPool = new Queue<GameObject>();
    public int maxCnt;
    public int defaultCnt;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    public void Init(){
        GameObject tmp;
        GameObject tmp1;
        for(int i = 1;i<=defaultCnt;i++){
            tmp = Instantiate(friendLaser,this.transform);
            tmp1 = Instantiate(enemyLaser,this.transform);
            friendPool.Enqueue(tmp);
            friendPool.Enqueue(tmp1);
            tmp.SetActive(false);
            tmp1.SetActive(false);
        }
    }

    public GameObject Pop(){
        GameObject tmp;
        if(friendPool.Count>0){
            tmp = friendPool.Dequeue();
            tmp.SetActive(true);
        }else{
            tmp = Instantiate(friendLaser,this.transform);
        }
        return tmp;
    }

    public void Push(GameObject tmp){
        if(friendPool.Count<=maxCnt){
            if(!friendPool.Contains(tmp)){
                tmp.SetActive(false);
                friendPool.Enqueue(tmp);
            }
        }else{
            Destroy(tmp);
        }
    }
    public GameObject PopE(){
        GameObject tmp;
        if(enemyPool.Count>0){
            tmp = enemyPool.Dequeue();
            tmp.SetActive(true);
        }else{
            tmp = Instantiate(enemyLaser,this.transform);
        }
        return tmp;
    }

    public void PushE(GameObject tmp){
        if(enemyPool.Count<=maxCnt){
            if(!enemyPool.Contains(tmp)){
                tmp.SetActive(false);
                enemyPool.Enqueue(tmp);
            }
        }else{
            Destroy(tmp);
        }
    }
}
