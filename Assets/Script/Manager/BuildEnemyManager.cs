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
    [Tooltip("初始默认数量")]
    public int defaultCnt = 4;

    public float createArea;

    private bool isCreateInit = false;

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
        CreateInit(defaultCnt);


    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {

        if(isCreateInit){
            if(curCnt<maxCnt-defaultCnt+1){
                IEcreateEnemy = StartCoroutine(CreateEnemy());
            }
            if(curCnt>=maxCnt){
                StopCoroutine(IEcreateEnemy);
            }
        }

    }

    public void CreateInit(int cnt){
        while(curCnt<cnt){
            Vector3 pos = new Vector3(Random.Range(-createArea,createArea),Random.Range(-createArea,createArea),Random.Range(-createArea,createArea));
            GameObject enemy = GameObjectPool.Instance.Pop(enemyList[Random.Range(0,enemyList.Count-1)]);
            enemy.transform.position = pos;
            curCnt++;
        }
        isCreateInit = true;
    }

    IEnumerator CreateEnemy(){
        while(curCnt<maxCnt){
            Debug.Log("Createing");
            Vector3 pos = new Vector3(Random.Range(-createArea,createArea),Random.Range(-createArea,createArea),Random.Range(-createArea,createArea));
            GameObject enemy = GameObjectPool.Instance.Pop(enemyList[Random.Range(0,enemyList.Count-1)]);
            enemy.transform.position = pos;
            curCnt++;
            yield return new WaitForSeconds(repeatTime);
        }
    }

    //Debug
    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(this.transform.position,new Vector3(2*createArea,2*createArea,2*createArea));

    }







}
