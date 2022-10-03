using UnityEngine;
using System.Collections;

public class destroyMeImp : MonoBehaviour{

    float timer;
    public float deathtimer = 3;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;

        if(timer>=deathtimer)
        {
            Destroy(gameObject);
            timer = 0;
        }
	
	}
}
