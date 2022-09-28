using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BoatData",menuName = "Data/BoatData",order = 1)]
public class BoatSO : ScriptableObject
{

    [Header("速度")]
    public float brakeSpeed;
    public float rotSpeed;
    public float curSpeed;
    public float maxSpeed;
    public float accSpeed;

    [Header("油量")]
    public float curOil;
    public float maxOil;

    [Header("状态")]
    public bool isLeft;
    public bool isRight;
    public bool isUp;
    public bool isDown;
    public bool isForward;
    public bool isBrake;
    public bool isUnpower;

}
