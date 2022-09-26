using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sigleton<T> : MonoBehaviour where T:Sigleton<T>
{
    private static T instance;
    public static T Instance{
        get {return instance;}
    }
    public static bool isInit{
        get{return instance!=null;}
    }

    protected virtual void Awake(){
        if(isInit)Destroy(this);
        else instance = (T)this;
    }

    protected virtual void onDestory(){
        if(instance==this)instance = null;
    }
}
