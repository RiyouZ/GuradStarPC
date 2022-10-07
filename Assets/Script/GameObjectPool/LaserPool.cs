using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPool : Sigleton<LaserPool>
{
    public Transform origin;
    public GameObject Laser;
    public Queue<GameObject> pool = new Queue<GameObject>();
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
        for(int i = 1;i<=defaultCnt;i++){
            tmp = Instantiate(Laser,this.transform);
            pool.Enqueue(tmp);
            tmp.SetActive(false);
        }
    }

    public GameObject Pop(){
        GameObject tmp;
        if(pool.Count>0){
            tmp = pool.Dequeue();
            tmp.SetActive(true);
        }else{
            tmp = Instantiate(Laser,this.transform);
        }
        return tmp;
    }

    public void Push(GameObject tmp){
        if(pool.Count<=maxCnt){
            if(!pool.Contains(tmp)){
                tmp.SetActive(false);
                pool.Enqueue(tmp);
                tmp.transform.position = origin.transform.position;
            }
        }else{
            Destroy(tmp);
        }
    }
}
