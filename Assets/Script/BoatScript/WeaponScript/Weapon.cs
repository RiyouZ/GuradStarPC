using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public RayAim ray;
    public RaycastHit hitInfo;

    public GameObject targetPos;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    protected virtual void Awake()
    {
        ray = GameObject.FindWithTag("Target").GetComponent<RayAim>();
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    protected virtual void Update()
    {

        Debug.DrawLine(transform.position,targetPos.transform.position,Color.red);
    }

    public virtual void Shoot(){
        if(targetPos==null)Debug.LogError("未找到瞄准点");
        AudioManager.Instance.Play("Shoot");
        GameObject laser = LaserPool.Instance.Pop(targetPos.transform.position);
        laser.transform.position = transform.position;
        laser.GetComponent<Laser>().shootTarget = targetPos.transform.position;
        Debug.Log("射击目标坐标："+targetPos.transform.position);
    }

    public virtual void Shoot(Transform target,Transform origin){
        AudioManager.Instance.Play("Shoot");
        GameObject laser = LaserPool.Instance.Pop(target.position);
        laser.transform.position = origin.position;
        laser.GetComponent<Laser>().shootTarget = target.position;
        
    }


    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawLine(transform.position,targetPos.transform.position);

    }

}
