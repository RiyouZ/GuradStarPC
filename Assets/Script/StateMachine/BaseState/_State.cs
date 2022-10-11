using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class _State<T>
{
    //进入状态
    public abstract void Enter(T target);
    
    //持续状态
    public abstract void Execute(T target);

    //退出状态
    public abstract void Exit(T target);



}
