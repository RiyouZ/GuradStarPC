using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FB_GOBACK : _State<FriendBoat>
{
    private float timer;
    public override void Enter(FriendBoat target)
    {
        target.shootTarget = GameObject.FindObjectOfType<Boat>().gameObject;
    }

    public override void Execute(FriendBoat target)
    {
        if(target.IsPlayerInArea()){
            target.ChangeState(FBState.Chase);
        }
        target.ChaseTarget(target.shootTarget.transform);
        


    }

    public override void Exit(FriendBoat target)
    {
        
    }

}
