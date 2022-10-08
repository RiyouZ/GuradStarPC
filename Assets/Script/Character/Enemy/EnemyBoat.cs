using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoat : Enemy
{
    //随机掉落物列表
    public List<string> suppliesList;
    
    protected override void Awake(){
        base.Awake();
    }

    protected override void OnEnable(){
        //base.OnEnable();
    }



    protected override void Start(){
        base.Start();
    }

    protected override void Update()
    {
        if(state.CurHealth<=0){
            BuildEnemyManager.Instance.curCnt--;
            GameManager.Instance.enemyCnt--;
            GameObject supplies = GameObjectPool.Instance.Pop(suppliesList[Random.Range(0,suppliesList.Count)].ToString());
            supplies.transform.position = this.transform.position;
            GameObjectPool.Instance.Push(gameObject);
            //Destroy(gameObject);
        }
    }

}
