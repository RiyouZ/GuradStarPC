using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilSupplies : MonoBehaviour
{
    public SuppliesStats oilSuppliesState;

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
            //SuppliesPool.Instance.push(gameObject);
            Destroy(gameObject);
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
            Destroy(gameObject);
        }
        
    }

    public void Init(){
        oilSuppliesState = GetComponent<SuppliesStats>();
    }






}
