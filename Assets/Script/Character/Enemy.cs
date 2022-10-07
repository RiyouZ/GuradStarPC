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

    }
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    protected virtual void Start()
    {
       Init(); 
    }
    
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    protected virtual void Update()
    {
        if(state.CurHealth<=0){
            //EnemyPool.Instance.push(gameObject);
            Destroy(gameObject);
        }
    }


    protected void Init(){
        state = GetComponent<CharacterStats>();
        state.CurHealth = state.MaxHealth;
    }




}
