using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public CharacterStats state;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    protected virtual void Awake()
    {
       Init(); 
    }

    protected void Init(){
        state = GetComponent<CharacterStats>();
        state.CurHealth = state.MaxHealth;
    }




}
