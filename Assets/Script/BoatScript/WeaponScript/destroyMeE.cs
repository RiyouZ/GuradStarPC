using UnityEngine;
using System.Collections;

public class destroyMeE : MonoBehaviour{

    float timer;
    public float deathtimer = 10;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer -= Time.deltaTime;

        if(timer<=0)
        {
            LaserPool.Instance.PushE(this.gameObject);
            timer = deathtimer;
        }
	
	}
}
