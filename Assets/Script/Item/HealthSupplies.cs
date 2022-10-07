using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSupplies : MonoBehaviour
{
    public SuppliesStats HealthSuppliesState;

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
        HealthSuppliesState.CurLifeTime+=Time.deltaTime;
        if(HealthSuppliesState.CurLifeTime>=HealthSuppliesState.MaxLifeTime){
            GameObjectPool.Instance.Push(gameObject);
            //Destroy(gameObject);
        }
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.CompareTag("PlayerBoat")){
            HealthSuppliesState.Recover(GameObject.FindObjectOfType<Player>().state);
            Destroy(gameObject);
        }
        
    }

    public void Init(){
        HealthSuppliesState = GetComponent<SuppliesStats>();
    }

}
