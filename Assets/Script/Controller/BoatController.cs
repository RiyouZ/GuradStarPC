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
    
    public void OperateShoot(){
        PlayerManager.Instance.player.CurCoolTime-=Time.deltaTime;
        //Debug
        Debug.Log(PlayerManager.Instance.player.CurCoolTime);
        if(PlayerManager.Instance.player.CurCoolTime>0)return;
        Debug.Log("TimeReady");
        if(!PlayerManager.Instance.player.IsShoot)return;
        boat.Shoot();
        //RayToTarget();
        PlayerManager.Instance.player.CurCoolTime = PlayerManager.Instance.player.CoolTime;
    }

    //Debug
    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    private void OnDrawGizmos()
    {
        //武器检测
        Gizmos.color = Color.blue;
       // Gizmos.DrawRay(weapon.originPoint.transform.position,weapon.originPoint.transform.forward*100);
    }

}
