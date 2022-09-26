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

    }

    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    private void LateUpdate()
    {
        RotationClamp();
    }

    public void RotationClamp(){
        Vector3 tmp = transform.rotation.eulerAngles;
        Vector3 clampAngleMax = new Vector3(45,0,45);
        Vector3 clampAngleMin = new Vector3(-45,0,-45);
        tmp.x = Mathf.Clamp(tmp.x,clampAngleMin.x,clampAngleMax.x);
        tmp.z = Mathf.Clamp(tmp.z,clampAngleMin.z,clampAngleMax.z);
        Quaternion angle = Quaternion.Euler(tmp);
        transform.rotation = angle;

    }

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

    IEnumerator ResetRob(){
        while(!IsReset()){
            transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(0,0,0),resetSpeed*Time.deltaTime);
            Debug.Log(transform.rotation.eulerAngles);
            yield return null;
        }
    }

}
