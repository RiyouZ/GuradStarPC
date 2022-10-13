using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : Weapon
{
    //攻击间隔
    public float defaultShootTimer;
    public float shootTimer;
    protected override void Awake()
    {
        shootTimer = defaultShootTimer;

    }
    public override void Shoot(Transform target,Transform origin)
    {
        shootTimer-=Time.deltaTime;
        if(shootTimer<=0){
            AudioManager.Instance.Play("Shoot");
            GameObject laser = LaserPool.Instance.PopE();
            laser.transform.position = origin.position;
            laser.GetComponent<LaserE>().shootTarget = target.position;
            shootTimer = defaultShootTimer;
        }
    }
    

}
