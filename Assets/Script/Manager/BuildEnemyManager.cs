using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildEnemyManager : Sigleton<BuildEnemyManager>
{
    public GameObjectListSO enemyList;
    public Transform playerPos;

    public float repeatTime;

    public int maxCnt;
    public int curCnt;

    protected override void Awake(){
        base.Awake();
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        InvokeRepeating("CreateEnemy",0.5f,repeatTime); 
    }


    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        
    }
    public void CreateEnemy(){
        if(PlayerManager.Instance.player.isDead){
            CancelInvoke("CreateEnemy");
            return;
        }
        Vector3 pos = new Vector3(Random.Range(-500,500),Random.Range(-500,500),Random.Range(-500,500));
        if(pos==playerPos.position){
            return;
        }
        GameObject enemy = GameObjectPool.Instance.Pop(enemyList.ObjectName[Random.Range(0,enemyList.ObjectName.Count-1)]);
        enemy.transform.position = pos;
        curCnt++;
        if(curCnt>=maxCnt){
            CancelInvoke("CreateEnemy");
            return;
        }

    
    }








}
