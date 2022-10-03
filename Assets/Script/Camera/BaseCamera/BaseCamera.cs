using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCamera : MonoBehaviour
{
    private GameObject playerCamera;

    private Transform MyCameraTs;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        playerCamera = GameObject.Find("HeadCamera");
        MyCameraTs = GetComponent<Transform>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {

        
    }

    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    private void LateUpdate()
    {
        FollowCamera(playerCamera.GetComponent<Transform>());
        
    }

    public void FollowCamera(Transform follow){
        MyCameraTs.localEulerAngles = follow.localEulerAngles;
    }


}
