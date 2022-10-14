using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EB_DEAD : _State<EnemyBoat>
{
    public override void Enter(EnemyBoat target)
    {
        target.DestroyClipPlay();
        BuildEnemyManager.Instance.curCnt--;
        GameManager.Instance.enemyCnt--;
        GameObject supplies = GameObjectPool.Instance.Pop(target.suppliesList[Random.Range(0,target.suppliesList.Count)]);
        supplies.transform.position = target.transform.position;
        //if(EnemyManager.Instance.enemys.Count!=0)EnemyManager.Instance.DeRegisterEnemy(target,target.state);
        FriendManager.Instance.RemoveTargetList(target.transform.gameObject);
        GameObjectPool.Instance.Push(target.gameObject);
    }

    public override void Execute(EnemyBoat target)
    {
        
    }

    public override void Exit(EnemyBoat target)
    {
        
    }
}
