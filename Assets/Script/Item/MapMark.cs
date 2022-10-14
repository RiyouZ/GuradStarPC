using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMark : MonoBehaviour
{

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        GameTool.LockRotation(this.transform,Vector3.zero);
    }


}
