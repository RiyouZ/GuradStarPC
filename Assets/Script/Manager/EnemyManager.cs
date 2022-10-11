using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Sigleton<EnemyManager>
{
    public List<CharacterStats> enemys = new List<CharacterStats>();
    public List<EnemyBoat> enemyBoats = new List<EnemyBoat>();
    protected override void Awake()
    {
        base.Awake();
    }

    public void RegisterEnemy(EnemyBoat boat,CharacterStats value){
        enemyBoats.Add(boat);
        enemys.Add(value);
    }

    public void DeRegisterEnemy(EnemyBoat boat,CharacterStats value){
        enemyBoats.Remove(boat);
        enemys.Remove(value);
    }
    
    public void AddTargetList(GameObject target){
        Debug.Log("AddInManager"+target.name);
        foreach(var boat in enemyBoats){
            boat.shootTargetList.Add(target);
            Debug.Log(boat);
        }
    }




}
