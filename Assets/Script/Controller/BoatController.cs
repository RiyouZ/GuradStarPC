using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : Sigleton<BoatController>
{

    public Boat boat;


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



    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void FixedUpdate()
    {   
        OperateBoatForward();

    }

    public void InitBoat(){
        boat = GameObject.Find("BoatHull").GetComponent<Boat>();

    }

    public void OperateBoatRotation(){
        if(boat.boatState.IsDown){

            boat.RotDown();
        }
        if(boat.boatState.IsUp){

            boat.RotUp();
        }
        if(boat.boatState.IsLeft){

            boat.RotLeft();
        }
        if(boat.boatState.IsRight){

            boat.RotRight();
        }
        
    }
    
    public void OperateBoatForward(){
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

}
