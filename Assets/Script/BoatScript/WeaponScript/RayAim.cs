using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayAim : MonoBehaviour
{
    public Camera camera;
    public RaycastHit hitInfo;

    public Vector3 target;
    // Update is called once per frame
    void Update()
    {

        Ray ray = new Ray(camera.transform.position,transform.position-camera.transform.position);
        if(Physics.Raycast(ray,out hitInfo))
            target = hitInfo.point;
        else
            target = (transform.position-camera.transform.position)*200;
    }
    //Debug
    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(camera.transform.position,(transform.position-camera.transform.position)*100);

    }

}
