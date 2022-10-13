using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildEnemyManager : Sigleton<BuildEnemyManager>
{
    public List<GameObject> enemyList= new List<GameObject>();
    public Transform playerPos;

    public int repeatTime;

    public int maxCnt;
    public int curCnt;

    public float createArea;

    public Coroutine IEcreateEnemy;

    protected override void Awake(){
        base.Awake();
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        if(curCnt<maxCnt){
            IEcreateEnemy = StartCoroutine(CreateEnemy());
        }else{
            StopCoroutine(IEcreateEnemy);
        }

    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {


    }

    IEnumerator CreateEnemy(){
        while(curCnt<maxCnt){
            Vector3 pos = new Vector3(Random.Range(-createArea,createArea),Random.Range(-createArea,createArea),Random.Range(-createArea,createArea));
            GameObject enemy = GameObjectPool.Instance.Pop(enemyList[Random.Range(0,enemyList.Count-1)]);
            enemy.transform.position = pos;
            curCnt++;
            yield return new WaitForSeconds(repeatTime);
        }
    }








}
