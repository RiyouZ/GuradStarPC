using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : Sigleton<BoatController>
{

    public GameObject boat;
    public BoatStats boatState;
    //旋转需采用局部坐标
    public Transform boatRot;

    public Transform boatTs;
    public Rigidbody boatRb;

    [SerializeField]
    private float rotX = 0;
    private float rotZ = 0;




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
        boat = GameObject.Find("BoatHull");
        boatRot = GameObject.Find("BoatHull").GetComponent<Transform>();
        boatState = boat.GetComponent<BoatStats>();
        boatTs = boat.GetComponent<Transform>();
        boatRb = boat.GetComponent<Rigidbody>();
        boatRb.velocity = Vector3.zero;
        boatState.CurOil = boatState.MaxOil;
        boatState.IsUp = false;
        boatState.IsDown = false;
        boatState.IsLeft = false;
        boatState.IsRight = false;
        boatState.IsForward = false;
        boatState.IsBrake = false;
    }

    public void OperateBoatRotation(){
        if(boatState.IsDown){

            boatRot.Rotate(new Vector3(boatState.RotSpeed*Time.deltaTime,0,0));
        }
        if(boatState.IsUp){

            boatRot.Rotate(new Vector3(-boatState.RotSpeed*Time.deltaTime,0,0));
        }
        if(boatState.IsLeft){

            boatRot.Rotate(new Vector3(0,0,boatState.RotSpeed*Time.deltaTime));
        }
        if(boatState.IsRight){

            boatRot.Rotate(new Vector3(0,0,-boatState.RotSpeed*Time.deltaTime));
        }
        
    }
    
    public void OperateBoatForward(){
        if(boatState.IsForward){

            // Vector3 direct = (forwardPoint.localPosition-boatTs.position).normalized;
            boatState.CurSpeed = boatState.CurSpeed+boatState.AccSpeed;
            // boatRb.AddForceAtPosition(direct,forwardPoint.localPosition);

            // if(boatState.IsDown){
            //     boatRb.velocity = new Vector3(0,-1,1)*boatState.CurSpeed*Time.fixedDeltaTime;
            // }else if(boatState.IsUp){
            //     boatRb.velocity = new Vector3(0,1,1)*boatState.CurSpeed*Time.fixedDeltaTime;
            // }else if(boatState.IsLeft){
            //     boatRb.velocity = new Vector3(1,0,1)*boatState.CurSpeed*Time.fixedDeltaTime;
            // }else if(boatState.IsRight){
            //     boatRb.velocity = new Vector3(-1,0,1)*boatState.CurSpeed*Time.fixedDeltaTime;
            // }else{

            // }

           boatRb.AddRelativeForce(Vector3.forward*boatState.CurSpeed*Time.fixedDeltaTime);
        }else{
            if(boatState.IsBrake){
                boatState.CurSpeed = boatState.CurSpeed-boatState.BrakeSpeed;
            }else{
                boatState.CurSpeed = boatState.CurSpeed-boatState.AccSpeed;

            }
            boatRb.AddRelativeForce(Vector3.forward*boatState.CurSpeed*Time.fixedDeltaTime);
        }
        if(boatState.CurSpeed==0)boatRb.velocity = Vector3.zero;
    }

    public void IsForward(bool value){
        boatState.IsForward = value;
    }


    public void IsBrake(bool value){
        boatState.IsBrake = value;
    }

}
