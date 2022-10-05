using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilSupplies : MonoBehaviour
{
    public OilSuppliesStats oilSuppliesState;

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        oilSuppliesState.CurLifeTime+=Time.deltaTime;
        if(oilSuppliesState.CurLifeTime>=oilSuppliesState.MaxLifeTime){
            
        }
    }


    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.CompareTag("PlayerBoat")){
            oilSuppliesState.Recover(coll.gameObject.GetComponent<BoatStats>());
        }
        
    }

    public void Init(){
        oilSuppliesState = GetComponent<OilSuppliesStats>();
    }






}
