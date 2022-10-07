using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Sigleton<GameManager>
{
    public CharacterStats player;
    public BoatStats boat;
    public int enemyCnt;
    
    

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
    }

    public void Init(){
        enemyCnt = BuildEnemyManager.Instance.maxCnt;
    }



}
