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
            base.Shoot(target,origin);
            shootTimer = defaultShootTimer;
        }
    }
    

}
