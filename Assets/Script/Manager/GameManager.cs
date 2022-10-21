using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Sigleton<GameManager>
{
    public enum GameState{
        DEFAULT,
        WIN,
        DEFEAT,
        PAUSE,
        CONTINUE
    };

    public GameState gameState = GameState.DEFAULT;
    public CharacterStats player;
    public CharacterStats enemy;
    public BoatStats boat;

    public GameObject Line;
    public int enemyCnt;

    public bool isPause;
    
    

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        //GamePause();
        Line = GameObject.Find("PlayerLine");
    }

    private void Start() {
        Line.SetActive(false);
        
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        ListenGameState();
    }

    public void Init(){

    }

    public void ListenGameState(){
        if(player.CurHealth<=0){
            gameState = GameState.DEFEAT;
        }else if(enemyCnt<=0){
            gameState = GameState.WIN;
        }else if(isPause){
            gameState = GameState.PAUSE;
        }else if(!isPause){
            gameState = GameState.CONTINUE;
        }else{
            return;
        }
        switch(gameState){
            case GameState.WIN:
                Debug.LogWarning("Win!");
                WinGame();
                Line.SetActive(true);
                break;
            case GameState.DEFEAT:
                Debug.LogWarning("Deafeat!");
                Line.SetActive(true);
                DefeatGame();
                break;
            case GameState.PAUSE:
                PauseMenuGame();
                Line.SetActive(true);
                break;
            case GameState.CONTINUE:
                ContinueGame();
                Line.SetActive(false);
                break;
        }

    }
    public void GameStart(){
        Time.timeScale = TimeManager.Instance.scaleTime;
    }

    public void GamePause(){
        Time.timeScale = 0;
    }

    public void SetVRPause(bool value){
        isPause = value;
    }

    public void SetPause(){
        Debug.Log("启动菜单");
        isPause = true;
    }

    public void SetContinue(){
        isPause = false;
    }

    public void WinGame(){
        GamePause();
        UIManager.Instance.OpenUI("WinCanvas");
        UIManager.Instance.SetText("WinCanvas","Panel/EndPanel/Time",TimeManager.Instance.gameTime.ToString("0.00"));
        UIManager.Instance.SetText("WinCanvas","Panel/EndPanel/Score",player.socore.ToString());
    }
    public void DefeatGame(){
        GamePause();
        UIManager.Instance.OpenUI("DefeatCanvas");
        UIManager.Instance.SetText("DefeatCanvas","Panel/EndPanel/Time",TimeManager.Instance.gameTime.ToString("0.00"));
        UIManager.Instance.SetText("DefeatCanvas","Panel/EndPanel/Score",player.socore.ToString());
    }
    public void PauseMenuGame(){
        GamePause();
        UIManager.Instance.OpenUI("MenuCanvas");
    }
    public void ContinueGame(){
        UIManager.Instance.CloseUI("MenuCanvas");
        GameStart();
    }

    public void RestartGame(){
        //重置场景
        UIManager.Instance.UILoadSence("GameSence");
    }
    //退出到主菜单
    public void QuitToMenuGame(){
        
    }


}
