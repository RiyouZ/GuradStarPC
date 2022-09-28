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
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void FixedUpdate()
    {
        



    }
    public void InitBoat(){
        boat = GameObject.Find("Boat");
        boatState = boat.GetComponent<BoatStats>();
        boatTs = boat.GetComponent<Transform>();
        boatRb = boat.GetComponent<Rigidbody>();
        boatRb.velocity = Vector3.zero;
    }

    public void OperateBoat(){
        if(boatState.IsDown){
            
        }
        if(boatState.IsUp){

        }
        if(boatState.IsLeft){

        }
        if(boatState.IsRight){

        }



    }
    




}
