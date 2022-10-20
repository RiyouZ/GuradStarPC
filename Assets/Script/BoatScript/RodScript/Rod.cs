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
        //rb.isKinematic = true;
        //ts.localPosition = Vector3.zero;
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
            BoatController.Instance.boat.boatState.IsUp = true;
            ColorChangeManager.Instance.ChangeColor("ButtonDown",Color.red);
        }
        if (coll.CompareTag("DownArea")){
            BoatController.Instance.boat.boatState.IsDown = true;
            ColorChangeManager.Instance.ChangeColor("ButtonUp",Color.red);
        }
        if (coll.CompareTag("LeftArea")){
            BoatController.Instance.boat.boatState.IsLeft = true;
            ColorChangeManager.Instance.ChangeColor("ButtonLeft",Color.red);
        }
        if (coll.CompareTag("RightArea")){
            BoatController.Instance.boat.boatState.IsRight = true;
            ColorChangeManager.Instance.ChangeColor("ButtonRight",Color.red);
        }
        if (coll.CompareTag("UpAndLeftArea")){
            BoatController.Instance.boat.boatState.IsUpAndLeft = true;
            ColorChangeManager.Instance.ChangeColor("ButtonUp",Color.red);
            ColorChangeManager.Instance.ChangeColor("ButtonLeft",Color.red);
        }
        if (coll.CompareTag("UpAndRightArea")){
            BoatController.Instance.boat.boatState.IsUpAndRight = true;
            ColorChangeManager.Instance.ChangeColor("ButtonUp",Color.red);
            ColorChangeManager.Instance.ChangeColor("ButtonRight",Color.red);
        }
        if (coll.CompareTag("DownAndLeftArea")){
            BoatController.Instance.boat.boatState.IsDownAndLeft = true;
            ColorChangeManager.Instance.ChangeColor("ButtonDown",Color.red);
            ColorChangeManager.Instance.ChangeColor("ButtonLeft",Color.red);
        }
        if (coll.CompareTag("DownAndRightArea")){
            BoatController.Instance.boat.boatState.IsDownAndRight = true;
            ColorChangeManager.Instance.ChangeColor("ButtonDown",Color.red);
            ColorChangeManager.Instance.ChangeColor("ButtonRight",Color.red);
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
            BoatController.Instance.boat.boatState.IsUp = false;
            ColorChangeManager.Instance.ChangeColor("ButtonDown",Color.white);
        }
        if (coll.CompareTag("DownArea")){
            BoatController.Instance.boat.boatState.IsDown = false;
            ColorChangeManager.Instance.ChangeColor("ButtonUp",Color.white);
        }
        if (coll.CompareTag("LeftArea")){
            BoatController.Instance.boat.boatState.IsLeft = false;
            ColorChangeManager.Instance.ChangeColor("ButtonLeft",Color.white);
        }
        if (coll.CompareTag("RightArea")){
            BoatController.Instance.boat.boatState.IsRight = false;
            ColorChangeManager.Instance.ChangeColor("ButtonRight",Color.white);
        }
        if (coll.CompareTag("UpAndLeftArea")){
            BoatController.Instance.boat.boatState.IsUpAndLeft = false;
            ColorChangeManager.Instance.ChangeColor("ButtonUp",Color.white);
            ColorChangeManager.Instance.ChangeColor("ButtonLeft",Color.white);
        }
        if (coll.CompareTag("UpAndRightArea")){
            BoatController.Instance.boat.boatState.IsUpAndRight = false;
            ColorChangeManager.Instance.ChangeColor("ButtonUp",Color.white);
            ColorChangeManager.Instance.ChangeColor("ButtonRight",Color.white);
        }
        if (coll.CompareTag("DownAndLeftArea")){
            BoatController.Instance.boat.boatState.IsDownAndLeft = false;
            ColorChangeManager.Instance.ChangeColor("ButtonDown",Color.white);
            ColorChangeManager.Instance.ChangeColor("ButtonLeft",Color.white);
        }
        if (coll.CompareTag("DownAndRightArea")){
            BoatController.Instance.boat.boatState.IsDownAndRight = false;
            ColorChangeManager.Instance.ChangeColor("ButtonDown",Color.white);
            ColorChangeManager.Instance.ChangeColor("ButtonRight",Color.white);
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
