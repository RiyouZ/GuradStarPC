using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager :Sigleton<PlayerManager>
{
    public CharacterStats player;
    public bool isHot;

    protected override void Awake()
    {
        base.Awake();
    }

    public void RegisterPlayer(CharacterStats value){
        player = value;
    }

    public void IsShoot(){
        player.IsShoot = true;
    }

    public void IsUnShoot(){
        player.IsShoot = false;
    }

    public void IsGrab(){
        player.IsGrab = true;
    }
    public void IsUngrab(){
        player.IsGrab = false;
    }

}
