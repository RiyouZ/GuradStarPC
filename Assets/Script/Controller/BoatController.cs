using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoatController : Sigleton<BoatController>
{

    public enum RotState{UP,DOWN,RIGHT,LEFT,NULL};
    public RotState rotState;
    public Boat boat;
    public Weapon weapon;
    private Ray weaponRay;
    private RaycastHit hitInfo;
    
    private Coroutine coolHotTimeCoroutine;

    private bool isHotTime;



    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        InitBoat();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        OperateBoatRotation();
        //完全过热状态
        if(PlayerManager.Instance.player.CurHotTime<=0)isHotTime = false;
        if(PlayerManager.Instance.player.CurHotTime>=PlayerManager.Instance.player.MaxHotTime){
            isHotTime = true;
            coolHotTimeCoroutine = StartCoroutine("CoolHotTime");
        }
        if(PlayerManager.Instance.player.CurHotTime == 0&&isHotTime){
            StopCoroutine(coolHotTimeCoroutine);
        }
        OperateShoot();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void FixedUpdate()
    {   
        OperateBoatForward();
    }
    //初始化
    public void InitBoat(){
        boat = GameObject.Find("BoatHull").GetComponent<Boat>();
        weapon = GameObject.Find("Weapon").GetComponent<Weapon>();
    }
    #region:基本移动逻辑
    public void OperateBoatRotation(){
        if(boat.boatState.IsDown){
            rotState = RotState.DOWN;
        }else if(boat.boatState.IsUp){
            rotState = RotState.UP;
        }else if(boat.boatState.IsLeft){
            rotState = RotState.LEFT;
        }else if(boat.boatState.IsRight){
            rotState = RotState.RIGHT;
        }else{
            rotState = RotState.NULL;
        }
        
        switch(rotState){
            case RotState.DOWN:
                boat.RotDown();
                break;
            case RotState.UP:
                boat.RotUp();
                break;
            case RotState.LEFT:
                boat.RotLeft();
                break;
            case RotState.RIGHT:
                boat.RotRight();
                break;
            case RotState.NULL:
                break;
        }
    }
    
    public void OperateBoatForward(){
        if(PlayerManager.Instance.player.IsGrab==false)return;
        if(boat.boatState.IsForward){
            boat.boatState.CurSpeed = boat.boatState.CurSpeed+boat.boatState.AccSpeed;
            boat.TsForward(boat.boatState.CurSpeed);
        }else{
            if(boat.boatState.IsBrake){
                boat.boatState.CurSpeed = boat.boatState.CurSpeed-boat.boatState.BrakeSpeed;
            }else{
                boat.boatState.CurSpeed = boat.boatState.CurSpeed-boat.boatState.AccSpeed;
            }
            boat.TsForward(boat.boatState.CurSpeed);
        }
        if(boat.boatState.CurSpeed==0)boat.TsBrake();
    }
    
    public void IsForward(bool value){
        boat.boatState.IsForward = value;
    }


    public void IsBrake(bool value){
        boat.boatState.IsBrake = value;
    }
    #endregion
    
    #region:攻击逻辑
    public void OperateShoot(){
        PlayerManager.Instance.player.CurCoolTime-=Time.deltaTime;
        if(PlayerManager.Instance.player.CurCoolTime<=0){
            if(PlayerManager.Instance.player.IsShoot&&PlayerManager.Instance.player.CurHotTime<PlayerManager.Instance.player.MaxHotTime&&!isHotTime){
                boat.Shoot();
                //武器加热
                PlayerManager.Instance.player.CurHotTime+=(Time.deltaTime*10);
                //射击间隔
                PlayerManager.Instance.player.CurCoolTime = PlayerManager.Instance.player.CoolTime;
            }
        }
        //武器冷却
        if(!PlayerManager.Instance.player.IsShoot&&PlayerManager.Instance.player.CurHotTime>0)
            PlayerManager.Instance.player.CurHotTime-=(Time.deltaTime*0.1f);


    }

    IEnumerator CoolHotTime(){
        while(PlayerManager.Instance.player.CurHotTime>0){
            PlayerManager.Instance.player.CurHotTime-=Time.deltaTime;
            yield return null;
        }
    } 



    #endregion




    //Debug
    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    private void OnDrawGizmos()
    {
        
    }

}
