using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EB_MOVE : _State<EnemyBoat>
{
    float timer;
    float x;
    float y;
    float z;
    Vector3 pos;
    public override void Enter(EnemyBoat target)
    {
        x = Random.Range(target.transform.position.x-target.attckRadius,target.transform.position.x+target.attckRadius);
        y = Random.Range(target.transform.position.y-target.attckRadius,target.transform.position.y+target.attckRadius);
        z = Random.Range(target.transform.position.z-target.attckRadius,target.transform.position.z+target.attckRadius);
        pos = new Vector3(x,y,z);
    }

    public override void Execute(EnemyBoat target)
    {
        if(target.IsTargetInArea()){
            target.ChangeState(EBState.Attack);
        }
    }

    public override void Exit(EnemyBoat target)
    {
        
    }
}
