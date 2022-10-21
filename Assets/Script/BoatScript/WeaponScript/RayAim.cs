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
        Ray ray = new Ray(transform.position,transform.forward);
        if(Physics.Raycast(ray,out hitInfo)){
            target = hitInfo.point;
        }
        else{
            target = new Vector3(transform.position.x,transform.position.y,transform.position.z+200);
        }

        Debug.DrawLine(transform.position,target-transform.position,Color.blue);
    }
    //Debug
    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        Gizmos.DrawRay(transform.position,target-transform.position);

    }

}
