using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : Sigleton<TimeManager>
{
    public DateTime startTime;
    public DateTime curTime;



    protected override void Awake(){
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        startTime = DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
        curTime = DateTime.Now;

    }

    public bool IsAfterOneSeconds(){
        if(curTime>=startTime.AddSeconds(1)){
            startTime = startTime.AddSeconds(1);
            Debug.Log("1");
            return true;
        }
        return false;
    }

}
