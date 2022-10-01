using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoatController : Sigleton<BoatController>
{

    public enum RotState{UP,DOWN,RIGHT,LEFT};
    public RotState rotState;
    public Boat boat;
    public Weapon weapon;

    public event Action<GameObject> ShootToEnemy;

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
        if(!PlayerManager.Instance.player.IsGrab)return;
        if(boat.boatState.IsDown){
            rotState = RotState.DOWN;
        }else if(boat.boatState.IsUp){
            rotState = RotState.UP;
        }else if(boat.boatState.IsLeft){
            rotState = RotState.LEFT;
        }else if(boat.boatState.IsRight){
            rotState = RotState.RIGHT;
        }else{
            return;
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
        }
        
    }
    
    public void OperateBoatForward(){
        if(!PlayerManager.Instance.player.IsGrab)return;
        if(boat.boatState.IsForward){
            boat.boatState.CurSpeed = boat.boatState.CurSpeed+boat.boatState.AccSpeed;
        }else{
            if(boat.boatState.IsBrake){
                boat.boatState.CurSpeed = boat.boatState.CurSpeed-boat.boatState.BrakeSpeed;
            }else{
                boat.boatState.CurSpeed = boat.boatState.CurSpeed-boat.boatState.AccSpeed;
            }
        }
        boat.TsForward(boat.boatState.CurSpeed);
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
        if(PlayerManager.Instance.player.CurCoolTime>0)return;
        if(!PlayerManager.Instance.player.IsShoot&&!PlayerManager.Instance.player.IsGrab)return;
        RayToTarget();
        PlayerManager.Instance.player.CurCoolTime = PlayerManager.Instance.player.CoolTime;
    }

    //武器射线检测
    public void RayToTarget(){
        weaponRay = new Ray(weapon.originPoint,weapon.transform.forward);
        bool isHit = Physics.Raycast(weaponRay,out hitInfo,1<<3);
        Debug.DrawRay(weaponRay.origin,weaponRay.direction,Color.blue);
        if(!isHit){
            //TODO:没有击中的特效绘制
            return;
        }
        GameObject target = hitInfo.collider.gameObject;
        if(target.CompareTag("Enemy")){
            ShootToEnemy?.Invoke(target);
            if(PlayerManager.Instance.player.TakeDamage(PlayerManager.Instance.player,target.GetComponent<Enemy>().state)<=0)Destroy(target);
        }

    }

    //Debug
    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(weaponRay);


    }

}
