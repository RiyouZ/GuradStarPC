using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FB_CHASE : _State<FriendBoat>
{
    //追击时间
    private float timer;
    public override void Enter(FriendBoat target)
    {
        Debug.Log(target.shootTargetList.Count);
        timer = Random.Range(5f,8f);
        if(target.shootTargetList.Count!=0)target.shootTarget = target.shootTargetList[Random.Range(0,target.shootTargetList.Count-1)];
    }

    public override void Execute(FriendBoat target)
    {
        if(target.shootTarget==null){
            Debug.LogError("未找到追踪对象");
            return;
        }
        timer-=Time.deltaTime;
        if(timer<=0){
            target.ChangeAttackTarget(target.shootTarget);
            target.EndChangeAttackTarget(target.shootTarget);
        }
        if(target.IsTargetInArea()
            &&Vector3.Dot(target.transform.forward,target.shootTarget.transform.position-target.transform.position)>=0){
                target.ChangeState(FBState.Attack);
        }else{
            target.ChangeAttackTarget(target.shootTarget);
            target.EndChangeAttackTarget(target.shootTarget);
        }
        target.ChaseTarget(target.shootTarget.transform);
    }

    public override void Exit(FriendBoat target)
    {
        
    }
}
