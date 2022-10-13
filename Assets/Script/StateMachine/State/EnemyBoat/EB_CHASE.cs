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
        timer = Random.Range(10f,15f);
        //随机追击目标
        if(target.shootTargetList.Count!=0)target.shootTarget = target.shootTargetList[Random.Range(0,target.shootTargetList.Count-1)];

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
        if(target.IsTargetInArea()
            &&Vector3.Dot(target.transform.forward,target.shootTarget.transform.position-target.transform.position)>=0){
                target.ChangeState(EBState.Attack);
        }else{
            target.ChangeAttackTarget(target.shootTarget);
            target.EndChangeAttackTarget(target.shootTarget);
        }
        target.ChaseTarget(target.shootTarget.transform);

    }

    public override void Exit(EnemyBoat target)
    {
        
    }
}
