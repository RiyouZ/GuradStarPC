using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : Sigleton<BoatController>
{

    public GameObject boat;
    public BoatStats boatState;
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
        boat = GameObject.Find("Boat");
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
            rotX += boatState.RotSpeed*Time.deltaTime;
            rotX = Mathf.Clamp(rotX,-90,90);
            Vector3 rot = new Vector3(rotX,boatTs.localEulerAngles.y,boatTs.localEulerAngles.z);
            boatTs.localEulerAngles = rot;
            Debug.Log("Down"+rot);
        }
        if(boatState.IsUp){
            rotX -= boatState.RotSpeed*Time.deltaTime;
            rotX = Mathf.Clamp(rotX,-90,90);
            Vector3 rot = new Vector3(rotX,boatTs.localEulerAngles.y,boatTs.localEulerAngles.z);
            boatTs.localEulerAngles = rot;
            Debug.Log("Up"+rot);
        }
        if(boatState.IsLeft){
            rotZ -= boatState.RotSpeed*Time.deltaTime;
            Vector3 rot = new Vector3(boatTs.localEulerAngles.x,boatTs.localEulerAngles.y,rotZ);
            boatTs.localEulerAngles = rot;
            Debug.Log("Left"+rot);
        }
        if(boatState.IsRight){
            rotZ += boatState.RotSpeed*Time.deltaTime;
            Vector3 rot = new Vector3(boatTs.localEulerAngles.x,boatTs.localEulerAngles.y,rotZ);
            boatTs.localEulerAngles = rot;
            Debug.Log("Right"+rot);
        }
        
    }
    
    public void OperateBoatForward(){
        if(boatState.IsForward){
            boatState.CurSpeed = boatState.CurSpeed+boatState.AccSpeed;
            if(boatState.IsDown){
                boatRb.velocity = new Vector3(1,0,1)*boatState.CurSpeed*Time.fixedDeltaTime;
            }else if(boatState.IsUp){
                boatRb.velocity = new Vector3(-1,0,1)*boatState.CurSpeed*Time.fixedDeltaTime;
            }else{
                boatRb.velocity = Vector3.forward*boatState.CurSpeed*Time.fixedDeltaTime;
            }


        }else{
            boatState.CurSpeed = boatState.CurSpeed-boatState.AccSpeed;
            boatRb.AddForce(Vector3.forward*boatState.CurSpeed*Time.fixedDeltaTime);
        }
        if(boatState.IsBrake){
            boatState.CurSpeed = boatState.CurSpeed-boatState.BrakeSpeed;
            boatRb.AddForce(Vector3.forward*-boatState.CurSpeed*Time.fixedDeltaTime);
        }
        if(boatState.CurSpeed==0)boatRb.velocity = Vector3.zero;
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
