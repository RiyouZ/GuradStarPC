using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EB_ATTACK : _State<EnemyBoat>
{
    //攻击时间
    private float timer;
    public override void Enter(EnemyBoat target)
    {
        timer = Random.Range(0.5f,1f);
    }

    public override void Execute(EnemyBoat target)
    {
        timer-=Time.deltaTime;
        
        if(timer<=0){
            target.ChangeState(EBState.Chase);
        }
        target.AttackToTarget(target.shootTarget);
        target.ChaseTarget(target.shootTarget.transform);
    }

    public override void Exit(EnemyBoat target)
    {
        
    }
}
