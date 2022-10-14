using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCamera : MonoBehaviour
{
    public Camera _camera;

    private Vector3 _lockRot = new Vector3(90,0,0); 
    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = _lockRot;
    }
}
