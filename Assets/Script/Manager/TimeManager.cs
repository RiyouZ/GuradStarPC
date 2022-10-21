using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : Sigleton<TimeManager>
{
    public DateTime startTime;
    public DateTime curTime;

    public float gameTime;

    public float scaleTime = 0.65f;



    protected override void Awake(){
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetGlobalTime(scaleTime);
    }

    // Update is called once per frame
    void Update()
    {
        gameTime+=Time.deltaTime;
    }

    
    public float GetGameTime(){
        return gameTime;
    }


    public bool IsAfterOneSeconds(){
        if(curTime>=startTime.AddSeconds(1)){
            startTime = startTime.AddSeconds(1);
            Debug.Log("1");
            return true;
        }
        return false;
    }

    public void SetGlobalTime(float t){
        Time.timeScale = t;
    }

}
