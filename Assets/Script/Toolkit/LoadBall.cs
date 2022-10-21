using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBall : Sigleton<LoadBall>
{
    public Material mat;
    public Color color;

    public Color preColor;
    private bool isLoad;
    protected override void Awake()
    {
        base.Awake();
        mat = GetComponent<MeshRenderer>().material;
        color = new Color(0,0,0,100);
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        
    }

    IEnumerator Load(){
        while(mat.color!=this.color){
            mat.color = Color.Lerp(mat.color,color,Time.deltaTime*0.02f);
            yield return null;
        }
        isLoad = true;
    }

    public void StartLoad(){
        StartCoroutine(Load());
    }


}
