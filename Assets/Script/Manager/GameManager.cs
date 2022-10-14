using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Sigleton<GameManager>
{
    public enum GameState{DEFAULT,WIN,DEFEAT};

    public GameState gameState = GameState.DEFAULT;
    public CharacterStats player;
    public CharacterStats enemy;
    public BoatStats boat;
    public int enemyCnt;
    
    

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {

    }

    public void Init(){

    }

    public void ListenGameState(){
        if(player.CurHealth<=0){
            gameState = GameState.DEFEAT;
        }else if(enemyCnt<=0){
            gameState = GameState.WIN;
        }else{
            return;
        }
        switch(gameState){
            case GameState.WIN:
                WinGame();
                break;
            case GameState.DEFEAT:
                DefeatGame();
                break;
        }

    }

    public void WinGame(){

    }
    public void DefeatGame(){

    }



}
