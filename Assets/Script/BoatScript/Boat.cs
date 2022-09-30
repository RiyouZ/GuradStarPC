using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    public Rigidbody rb;
    public BoatStats boatState;
    public Transform boatRot;

/// <summary>
/// Awake is called when the script instance is being loaded.
/// </summary>
    private void Awake()
    {
        Init();
    }


    public void Init(){
        boatRot = GetComponent<Transform>();
        boatState = GetComponent<BoatStats>();
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        boatState.CurSpeed = boatState.MaxSpeed;
        boatState.CurOil = boatState.MaxOil;
        boatState.IsUp = false;
        boatState.IsDown = false;
        boatState.IsLeft = false;
        boatState.IsRight = false;
        boatState.IsForward = false;
        boatState.IsBrake = false;
    }

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
    public void TsForward(float speed){
        rb.AddRelativeForce(Vector3.forward*speed*Time.fixedDeltaTime);
    }
    public void TsBrake(){
        rb.velocity = Vector3.zero;
    }



}
