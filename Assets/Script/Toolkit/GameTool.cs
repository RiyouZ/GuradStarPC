using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTool
{
    //转换实际角度
    public static float ChangeAngle(float value){
        float angle = value - 180;

        return angle>0?angle - 180:angle+180;
    }

    //锁定旋转
    public static void LockRotation(Transform ts,Vector3 lo){
        ts.eulerAngles = lo;
    }


}
