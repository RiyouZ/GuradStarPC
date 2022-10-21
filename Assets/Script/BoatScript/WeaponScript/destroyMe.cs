using UnityEngine;
using System.Collections;

public class destroyMe : MonoBehaviour{

    float timer;
    public float deathtimer = 10;


    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer -= Time.deltaTime;

        if(timer<=0)
        {
            LaserPool.Instance.Push(this.gameObject);
            timer = deathtimer;
        }
	
	}
}
