using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//状态机
public class StateMachine<T>
{
    private T target;
    private _State<T> preState;
    public _State<T> curState;

    public StateMachine(T target){
        this.target = target;
        this.preState = null;
        this.curState = null;
    }

    //进入状态
    public void SetCurState(_State<T> cur){
        this.curState = cur;
        this.curState.Enter(target);
    }
    //改变状态
    public void ChangeCurState(_State<T> cur){
        this.curState.Exit(target);
        this.preState = this.curState;
        this.curState = cur;
        this.curState.Enter(target);
    }

    //更新状态
    public void OnUpdate(){
        if(this.curState!=null){
            this.curState.Execute(target);
        }
    }

}
