using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EB_CHASE : _State<EnemyBoat>
{
    //追击时间
    private float timer;
    private Quaternion targetRot;
    public override void Enter(EnemyBoat target)
    {
        //随机追击目标
        target.shootTarget = target.shootTargetList[Random.Range(0,target.shootTargetList.Count-1)];
    }

    public override void Execute(EnemyBoat target)
    {
        if(target.shootTarget==null){
            Debug.LogError("未找到追踪对象");
            return;
        }
        timer-=Time.deltaTime;
        if(timer<=0){
            if(Random.Range(0f,1f)>=0.5){
                target.shootTarget = target.player;
            }else{
                target.ChangeAttackTarget(target.shootTarget);
                target.EndChangeAttackTarget(target.shootTarget);
            }
        }
        if(target.IsTargetInArea()){
            target.ChangeState(EBState.Attack);
        }
        target.ChaseTarget(target.shootTarget.transform);

    }

    public override void Exit(EnemyBoat target)
    {
        
    }
}
