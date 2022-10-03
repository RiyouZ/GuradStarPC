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
    private void Awake()
    {
        ray = GameObject.FindWithTag("Target").GetComponent<RayAim>();
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {


    }

    public void Shoot(){
        GameObject laser = LaserPool.Instance.Pop();
        laser.GetComponent<Laser>().shootTarget = ray.target;
    }    


}
