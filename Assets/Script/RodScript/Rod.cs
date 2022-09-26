using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Rod : MonoBehaviour
{
    private Vector3 origionPosition;
    private Rigidbody rb;
    private Transform ts;


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
        origionPosition = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.isKinematic = true;
        if(transform.position!=origionPosition)ts.Translate(Vector3.zero);
        if(!IsReset()&&!PlayerManager.Instance.player.IsGrab)transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.identity,resetSpeed*Time.deltaTime);

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
        RotationClamp();
    }
    /// <summary>
    /// 限制角度
    /// </summary>
    public void RotationClamp(){
        Vector3 rotation = new Vector3(GameTool.ChangeAngle(transform.rotation.eulerAngles.x),GameTool.ChangeAngle(transform.rotation.eulerAngles.y),GameTool.ChangeAngle(transform.rotation.eulerAngles.z));
        rotation.x = Mathf.Clamp(rotation.x,-13,13);
        rotation.z = Mathf.Clamp(rotation.z,-13,13);
        transform.eulerAngles = rotation;
        Debug.Log("Lock");
    }

    /// <summary>
    /// 是否回正
    /// </summary>
    /// <returns>bool</returns>
    public bool IsReset(){
        return transform.rotation.eulerAngles==Vector3.zero;
    }

    public void DoReset(){
        resetRob =  StartCoroutine("ResetRob");
        Debug.Log(resetRob);
    }

    public void StopReset(){
        StopCoroutine("ResetRob");
    }

    public void Init(){
        ts = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
    }


    #region 协程
    /// <summary>
    /// 摇杆回正
    /// </summary>
    /// <returns></returns>
    IEnumerator ResetRob(){
        while(!IsReset()){
            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.identity,resetSpeed*Time.fixedDeltaTime);
            Debug.Log(transform.rotation.eulerAngles);
            yield return null;
        }
    }
    #endregion
}
