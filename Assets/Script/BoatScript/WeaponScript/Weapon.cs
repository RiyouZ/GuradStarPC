using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    public Vector3 originPoint;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        Init();
    }
    private void Start()
    {

    }

    public void Init(){
        originPoint = GameObject.Find("WeaponPoint").GetComponent<Transform>().position;   
    }

    public void ShootEnemy(GameObject enemy){
        //TODO:：发射子弹
    }

    


}
