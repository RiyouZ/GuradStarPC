using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Rod : MonoBehaviour
{
    private Vector3 origionPosition;
    private Rigidbody rb;
    private Transform ts;
    private Transform pivot;


    [SerializeField]
    private float resetSpeed;




    #region 协程事件
    private Coroutine resetRob;

    #endregion

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        Init();

    }

    // Start is called before the first frame update
    void Start()
    {
        origionPosition = transform.localPosition;
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.isKinematic = true;
        ts.localPosition = Vector3.zero;
        //if(transform.localPosition!=origionPosition)ts.Translate(Vector3.zero);
        //复位
        if(!IsReset()&&!PlayerManager.Instance.player.IsGrab){
            transform.localRotation = Quaternion.Slerp(transform.localRotation,Quaternion.identity,resetSpeed*Time.deltaTime);
            // Vector3 dirt = ts.localEulerAngles.normalized;
        }

    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void FixedUpdate()
    {

        //if(IsReset())StopReset();
        
    }

    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    private void LateUpdate()
    {
        //限制角度
        RotationClamp();
    } 

    /// <summary>
    /// OnTriggerStay is called once per frame for every Collider other
    /// that is touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerEnter(Collider coll)
    {
        //操作区域检测
        if (coll.CompareTag("UpArea")){
            BoatController.Instance.boatState.IsUp = true;
        }
        if (coll.CompareTag("DownArea")){
            BoatController.Instance.boatState.IsDown = true;
        }
        if (coll.CompareTag("LeftArea")){
            BoatController.Instance.boatState.IsLeft = true;
        }
        if (coll.CompareTag("RightArea")){
            BoatController.Instance.boatState.IsRight = true;
        }
    }
    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerExit(Collider coll)
    {
        //操作区域检测
        if (coll.CompareTag("UpArea")){
            BoatController.Instance.boatState.IsUp = false;
        }
        if (coll.CompareTag("DownArea")){
            BoatController.Instance.boatState.IsDown = false;
        }
        if (coll.CompareTag("LeftArea")){
            BoatController.Instance.boatState.IsLeft = false;
        }
        if (coll.CompareTag("RightArea")){
            BoatController.Instance.boatState.IsRight = false;
        }

        
    }


    /// <summary>
    /// 限制角度
    /// </summary>
    public void RotationClamp(){
        Vector3 rotation = new Vector3(GameTool.ChangeAngle(transform.localEulerAngles.x),GameTool.ChangeAngle(transform.localEulerAngles.y),GameTool.ChangeAngle(transform.localEulerAngles.z));
        rotation.x = Mathf.Clamp(rotation.x,-13,13);
        rotation.z = Mathf.Clamp(rotation.z,-13,13);
        transform.localEulerAngles = rotation;
        Debug.Log("Lock");
    }

    /// <summary>
    /// 是否回正
    /// </summary>
    /// <returns>bool</returns>
    public bool IsReset(){
        return transform.localEulerAngles==Vector3.zero;
    }



    public void Init(){
        ts = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        pivot = GameObject.Find("RodPivot").GetComponent<Transform>();
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
    }


    #region 协程

    #endregion
}
