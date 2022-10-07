using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildEnemyManager : Sigleton<BuildEnemyManager>
{
    public GameObjectListSO enemyList;
    public Transform playerPos;

    public int maxCnt;
    public int curCnt;

    protected override void Awake(){
        base.Awake();
    }

    public void CreateEnemy(){
        if(PlayerManager.Instance.player.isDead){
            CancelInvoke("CreateEnemy");
            return;
        }
        GameObject enemy = GameObjectPool.Instance.Pop(enemyList.ObjectName[Random.Range(0,enemyList.ObjectName.Count)]);
        curCnt++;
        enemy.transform.position = new Vector3(playerPos.position.x+Random.Range(100,500),playerPos.position.y,playerPos.position.z);
    
    }








}
