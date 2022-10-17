using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Sigleton<GameManager>
{
    public enum GameState{
        DEFAULT,
        WIN,
        DEFEAT,
        PAUSE
    };

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
        //GamePause();
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
    public void GameStart(){
        Time.timeScale = 1;
    }

    public void GamePause(){
        Time.timeScale = 0;
    }


    public void WinGame(){
        GamePause();
        UIManager.Instance.OpenUI("WinCanvas");
    }
    public void DefeatGame(){
        GamePause();
        UIManager.Instance.OpenUI("DefeatCanvas");
    }
    public void PauseMenuGame(){
        GamePause();
        UIManager.Instance.OpenUI("MenuCanvas");
    }
    public void ContinueGame(){
        GamePause();
        UIManager.Instance.CloseUI("MenuCanvas");
    }


}
