using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSupplies : MonoBehaviour
{
    public SuppliesStats HealthSuppliesState;

    public Camera lookCamera;

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
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    private void LateUpdate()
    {
        LookCamera(lookCamera);
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.CompareTag("PlayerBoat")){
            HealthSuppliesState.Recover(GameObject.FindObjectOfType<Player>().state);
            HealthSuppliesState.AddSocore(GameObject.FindObjectOfType<Player>().state);
            Destroy(gameObject);
        }
        
    }

    public void Init(){
        HealthSuppliesState = GetComponent<SuppliesStats>();
        lookCamera = GameObject.Find("BoatCamera").GetComponent<Camera>();
    }

    //朝向相机
    public void LookCamera(Camera camera){
        Quaternion rot = Quaternion.identity;
        rot.SetLookRotation(camera.transform.forward,camera.transform.up);
        transform.rotation = rot;
    }



}
