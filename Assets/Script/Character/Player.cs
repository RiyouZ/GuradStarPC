using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterStats state;

    // Start is called before the first frame update

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        state = GetComponent<CharacterStats>();
    }
    void Start()
    {
        Init();
        PlayerManager.Instance.RegisterPlayer(state);
    }


    public void Init(){
        state.CurHealth = state.MaxHealth;
        state.IsGrab = false;
        state.IsShoot = false;
    }


}
