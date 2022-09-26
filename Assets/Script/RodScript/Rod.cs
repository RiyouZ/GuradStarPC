using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Rod : MonoBehaviour
{
    private Quaternion orginRotate;
    


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

    }

    // Start is called before the first frame update
    void Start()
    {
        orginRotate = transform.rotation;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsReset())StopReset();
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
        return transform.rotation==Quaternion.Euler(0,0,0);
    }

    public void DoReset(){
        resetRob =  StartCoroutine("ResetRob");
        Debug.Log(resetRob);
    }

    public void StopReset(){
        StopCoroutine("ResetRob");
    }


    #region 协程
    /// <summary>
    /// 摇杆回正
    /// </summary>
    /// <returns></returns>
    IEnumerator ResetRob(){
        while(!IsReset()){
            transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(0,0,0),resetSpeed*Time.deltaTime);
            Debug.Log(transform.rotation.eulerAngles);
            yield return null;
        }
    }
    #endregion
}
