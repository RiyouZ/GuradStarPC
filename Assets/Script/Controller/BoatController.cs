using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : Sigleton<BoatController>
{

    public GameObject boat;
    public BoatStats boatState;
    public Transform boatTs;
    public Rigidbody boatRb;


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
        boat = GameObject.Find("Boat");
        boatState = boat.GetComponent<BoatStats>();
        boatTs = boat.GetComponent<Transform>();
        boatRb = boat.GetComponent<Rigidbody>();
        boatRb.velocity = Vector3.zero;
    }

    public void OperateBoatRotation(){
        if(boatState.IsDown){
            float rot = boatState.RotSpeed;
            boatTs.Rotate(new Vector3(rot,0,0));
        }
        if(boatState.IsUp){
            float rot = -boatState.RotSpeed;
            boatTs.Rotate(new Vector3(rot,0,0));
        }
        if(boatState.IsLeft){
            float rot = boatState.RotSpeed;
            boatTs.Rotate(new Vector3(0,0,rot));
        }
        if(boatState.IsRight){
            float rot = -boatState.RotSpeed;
            boatTs.Rotate(new Vector3(0,0,rot));
        }

    }
    
    public void OperateBoatForward(){
        if(boatState.IsForward){
            boatState.CurSpeed = boatState.CurSpeed*boatState.AccSpeed;
            boatRb.AddForce(Vector3.forward*boatState.CurSpeed*Time.fixedDeltaTime);
        }
        if(boatState.IsBrake){
            boatState.CurSpeed = boatState.CurSpeed*-boatState.BrakeSpeed;
            boatRb.AddForce(Vector3.forward*-boatState.CurSpeed*Time.fixedDeltaTime);
        }

    }

    public void IsForward(){
        boatState.IsForward = true;
    }

    public void IsUnForward(){
        boatState.IsForward = false;
    }

    public void IsBrake(){
        boatState.IsBrake = true;
    }

    public void IsUnBrake(){
        boatState.IsBrake = false;
    }




}
