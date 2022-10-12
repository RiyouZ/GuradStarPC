using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public RayAim ray;
    public RaycastHit hitInfo;
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


    }

    public virtual void Shoot(){
        AudioManager.Instance.Play("Shoot");
        GameObject laser = LaserPool.Instance.Pop();
        laser.transform.position = this.transform.position;
        laser.GetComponent<Laser>().shootTarget = ray.target;
    }    

    public virtual void Shoot(Transform target,Transform origin){
        AudioManager.Instance.Play("Shoot");
        GameObject laser = LaserPool.Instance.Pop();
        laser.transform.position = origin.position;
        laser.GetComponent<Laser>().shootTarget = target.position;
    }


}
