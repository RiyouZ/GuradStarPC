using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public GameObject hud;

    public Image hudHealthBar;
    public Image hudSpeedBar;
    public Image hudBulletBar;
    public Image hudOilBar;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        Init();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        HUDUpdate();
    }

    public void Init(){
        hud = transform.GetChild(0).GetChild(0).gameObject;
        hudBulletBar = hud.transform.Find("Bullet").GetChild(1).GetComponent<Image>();
        hudHealthBar = hud.transform.Find("Health").GetChild(1).GetComponent<Image>();
        hudSpeedBar = hud.transform.Find("Speed").GetChild(1).GetComponent<Image>();
        hudOilBar = hud.transform.Find("Oil").GetChild(1).GetComponent<Image>();
    }

    public void HUDUpdate(){
        hudBulletBar.fillAmount = GameManager.Instance.player.CurHotTime/GameManager.Instance.player.MaxHotTime;
        hudHealthBar.fillAmount = GameManager.Instance.player.CurHealth/GameManager.Instance.player.MaxHealth;
        hudOilBar.fillAmount = GameManager.Instance.boat.CurOil/GameManager.Instance.boat.MaxOil;
        hudSpeedBar.fillAmount = GameManager.Instance.boat.CurSpeed/GameManager.Instance.boat.MaxSpeed;

    }


}
