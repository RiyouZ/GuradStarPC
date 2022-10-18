using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [Header("音效")]
    public AudioSource hitClip;
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
        hitClip = GetComponent<AudioSource>();
        
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        transform.LookAt(shootTarget);
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void FixedUpdate()
    {
        speed+=speed*multiSpeed;
        speed = Mathf.Clamp(speed,0,1000);
        rb.AddForce(transform.forward*speed,ForceMode.Impulse);
        //CheckCollision(shootTarget);
    }
    void CheckCollision(Vector3 prevPos)
    {
        RaycastHit hit;
        Vector3 direction = prevPos-transform.position;
        Ray ray = new Ray(transform.position, direction);
        //float dist = Vector3.Distance(transform.position, prevPos);

        if (Physics.Raycast(ray, out hit))
        {
            transform.position = hit.point;
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, hit.normal);
            Vector3 pos = hit.point;
            Instantiate(impact, pos, rot);
            coll.enabled = false;
            rb.velocity = Vector3.zero;
            LaserPool.Instance.Push(gameObject);

        }
    }
    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    private void OnCollisionEnter(Collision other)
    {
        if(!other.gameObject.CompareTag("FX")){
            // ContactPoint contact = other.contacts[0];
            // Quaternion rot = Quaternion.FromToRotation(Vector3.forward,contact.normal);
            // Vector3 pos = contact.point;
            // Instantiate(impact,pos,rot);
            if(other.gameObject.CompareTag("Enemy")){
                if(hitClip.isPlaying==false)hitClip.Play();
                float health = PlayerManager.Instance.player.TakeDamage(PlayerManager.Instance.player,
                    other.gameObject.GetComponent<EnemyBoat>().state
                    );
            }
            coll.enabled = false;
            rb.velocity = Vector3.zero;
            LaserPool.Instance.Push(gameObject);
        }
    }

    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(shootTarget,transform.position-shootTarget);
    }



}
