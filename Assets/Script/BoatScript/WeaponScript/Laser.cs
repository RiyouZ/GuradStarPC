using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject impact;
    public Rigidbody rb;

    private Collider coll;

    public bool isShoot;

    public Vector3 shootTarget;

    public float speed;
    public float multiSpeed;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void FixedUpdate()
    {
        speed+=speed*multiSpeed;
        speed = Mathf.Clamp(speed,0,1000);
        transform.LookAt(shootTarget);
        rb.AddForce(transform.forward*speed);
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("FX"))return;
        ContactPoint contact = other.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.forward,contact.normal);
        Vector3 pos = contact.point;
        Instantiate(impact,pos,rot);
        LaserPool.Instance.Push(this.gameObject);
        if(!other.gameObject.CompareTag("Enemy"))return;
        
        int health = PlayerManager.Instance.player.TakeDamage(PlayerManager.Instance.player,
            other.gameObject.GetComponent<Enemy>().state
            );
        Debug.Log(health);
        if(health<=0)Destroy(coll.gameObject);
    }

}
