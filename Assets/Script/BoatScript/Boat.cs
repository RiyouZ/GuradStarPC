using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{

    public enum OilState{ZERO,ONE,TWO};

    public OilState oilState;
    public Rigidbody rb;
    public BoatStats boatState;
    public Transform boatRot;
    public Weapon weapon;
    public Weapon waeponR;

    public float mutilAudioForwardValue;

    private float curTimer;
    private float spendTime;

/// <summary>
/// Awake is called when the script instance is being loaded.
/// </summary>
    private void Awake()
    {
        //Init();
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        Init();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        curTimer+=Time.deltaTime;
        if(curTimer>=spendTime){
            ChangeConsumeOilState(4,70,100);
            curTimer = 0;
        }
    }

    public void Init(){
        weapon = GameObject.Find("WeaponL").GetComponent<Weapon>();
        waeponR = GameObject.Find("WeaponR").GetComponent<Weapon>();
        boatRot = GetComponent<Transform>();
        boatState = GetComponent<BoatStats>();
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        boatState.CurSpeed = 0;
        boatState.CurOil = boatState.MaxOil;
        boatState.IsUp = false;
        boatState.IsDown = false;
        boatState.IsLeft = false;
        boatState.IsRight = false;
        boatState.IsForward = false;
        boatState.IsBrake = false;
    }
    //旋转
    public void RotDown(){
        boatRot.Rotate(new Vector3(boatState.RotSpeed*Time.deltaTime,0,0));
    }
    public void RotUp(){
        boatRot.Rotate(new Vector3(-boatState.RotSpeed*Time.deltaTime,0,0));
    }
    public void RotLeft(){
        boatRot.Rotate(new Vector3(0,0,boatState.RotSpeed*Time.deltaTime));
    }
    public void RotRight(){
        boatRot.Rotate(new Vector3(0,0,-boatState.RotSpeed*Time.deltaTime));
    }
    //前进
    public void TsForward(float speed){
        if(boatState.CurOil<=0)return;
        rb.AddRelativeForce(Vector3.forward*speed*Time.fixedDeltaTime);
    }
    public void TsBrake(){
        rb.velocity = Vector3.zero;
    }
    //攻击
    public void Shoot(){
        weapon.Shoot();
        waeponR.Shoot();
    }
    //修改耗油状态
    public void ChangeConsumeOilState(float firstOil,float secondOil,float thirdOil){
        if(0<boatState.CurSpeed&&boatState.CurSpeed<=boatState.MaxSpeed*(firstOil*0.01)){
            oilState = OilState.ZERO;           
        }else if(boatState.MaxSpeed*((firstOil+1)*0.01)<boatState.CurSpeed&&boatState.CurSpeed<=boatState.MaxSpeed*(secondOil*0.01)){
            oilState = OilState.ONE;
        }else if(boatState.MaxSpeed*((secondOil+1)*0.01)<boatState.CurSpeed&&boatState.CurSpeed<=boatState.MaxSpeed*(thirdOil*0.01)){
            oilState = OilState.TWO;
        }

        switch(oilState){
            case OilState.ZERO:
                boatState.ConsumeOil(0);
                if(AudioManager.Instance.IsPlay("Forward"))AudioManager.Instance.Stop("Forward");
                break;
            case OilState.ONE:
                boatState.ConsumeOil(0.0002f);
                if(!AudioManager.Instance.IsPlay("Forward")){
                    AudioManager.Instance.Play("Forward");
                }else{
                    AudioManager.Instance.SetPitch("Forward",boatState.CurSpeed*mutilAudioForwardValue);
                }
                Debug.Log(AudioManager.Instance.audioTag["Forward"].soruce.name);
                break;
            case OilState.TWO:
                boatState.ConsumeOil(0.0030f);
                if(!AudioManager.Instance.IsPlay("Forward")){
                    AudioManager.Instance.SetPitch("Forward",2);
                    AudioManager.Instance.Play("Forward");
                }else{
                    AudioManager.Instance.SetPitch("Forward",boatState.CurSpeed*mutilAudioForwardValue);
                }
                break;
        }
    }






}
