using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendWeapon : Weapon
{
    [Header("音效")]
    public AudioSource shootClip;

    [Header("属性")]
    //攻击间隔
    public float defaultShootTimer;
    public float shootTimer;
    protected override void Awake()
    {
        shootClip = GetComponent<AudioSource>();
        shootTimer = defaultShootTimer;

    }
    public override void Shoot(Transform target,Transform origin)
    {
        shootTimer-=Time.deltaTime;
        if(shootTimer<=0){
           GameObject laser = LaserPool.Instance.Pop();
            laser.transform.position = origin.position;
            laser.GetComponent<Laser>().shootTarget = target.position;
            shootTimer = defaultShootTimer;
        }
    }

}
