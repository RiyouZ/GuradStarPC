using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FB_ATTACK : _State<FriendBoat>
{

    private float timer;
    public override void Enter(FriendBoat target)
    {
        timer = Random.Range(0.5f,1f);
    }

    public override void Execute(FriendBoat target)
    {
        timer-=Time.deltaTime;
        
        if(timer<=0){
            target.ChangeState(FBState.Chase);
        }
        target.AttackToTarget(target.shootTarget);
        target.ChaseTarget(target.shootTarget.transform);
    }

    public override void Exit(FriendBoat target)
    {
        
    }

}
