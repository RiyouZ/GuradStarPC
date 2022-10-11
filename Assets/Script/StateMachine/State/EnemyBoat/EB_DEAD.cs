using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EB_DEAD : _State<EnemyBoat>
{
    public override void Enter(EnemyBoat target)
    {
        BuildEnemyManager.Instance.curCnt--;
        GameManager.Instance.enemyCnt--;
        GameObject supplies = GameObjectPool.Instance.Pop(target.suppliesList[Random.Range(0,target.suppliesList.Count)].ToString());
        supplies.transform.position = target.transform.position;
        if(EnemyManager.Instance.enemys.Count!=0)EnemyManager.Instance.DeRegisterEnemy(target,target.state);
        GameObjectPool.Instance.Push(target.transform.parent.gameObject);
    }

    public override void Execute(EnemyBoat target)
    {
        
    }

    public override void Exit(EnemyBoat target)
    {
        
    }
}
