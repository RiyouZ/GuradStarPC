using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTool
{
    public static float ChangeAngle(float value){
        float angle = value - 180;

        return angle>0?angle - 180:angle+180;

    }



}
