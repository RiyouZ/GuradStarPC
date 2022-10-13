using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum FBState{
    Chase,
    Attack,
    GoBack
}

public class FriendBoat : MonoBehaviour
{
    public Rigidbody rb;

    public FriendWeapon weaponL;
    public FriendWeapon weaponR;
    public CharacterStats state;

    //攻击目标对象列表
    public List<GameObject> shootTargetList = new List<GameObject>();
    //正在攻击的目标
    public GameObject shootTarget;
    //状态机
    public StateMachine<FriendBoat> machine;
    //存储状态
    public Dictionary<FBState,_State<FriendBoat>> stateDic;

    [Header("属性")]
    //攻击检测范围
    public float attckRadius;
    public float accRadius;
    public float accSpeed;
    public float maxAccSpeed;
    public float rotSpeed;
    public float maxRotSpeed;
    public float curSpeed;
    public float preSpeed = 0;
    [SerializeField]
    private int shootIdx;

    private static Coroutine IErandom;


    protected void Awake(){
        state = GetComponent<CharacterStats>();
        //rb = GetComponent<Rigidbody>();
        weaponL = transform.Find("WeaponFL").GetComponent<FriendWeapon>();
        weaponR = transform.Find("WeaponFR").GetComponent<FriendWeapon>();
    }

    protected  void OnEnable(){
        //base.OnEnable();
        //FriendManager.Instance.RegisterFriend(this.gameObject,state);
    }

    protected void Start(){
        EnemyManager.Instance.AddTargetList(this.gameObject);
        state.CurHealth = state.MaxHealth;
        //创建字典索引
        //if(shootTargetList.Count!=0)shootTarget = shootTargetList[0];
        stateDic = new Dictionary<FBState, _State<FriendBoat>>();
        stateDic.Add(FBState.Chase,new FB_CHASE());
        stateDic.Add(FBState.Attack,new FB_ATTACK());
        machine = new StateMachine<FriendBoat>(this);
        machine.SetCurState(stateDic[FBState.Chase]);
    }
    protected void Update()
    {
        machine.OnUpdate();
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("EdgeZone")){
            
            ChangeState(FBState.GoBack);
        }
    }

    public void ChangeState(FBState fbState){
        if(!stateDic.ContainsKey(fbState))return;
        machine.ChangeCurState(stateDic[fbState]);
    }
    public void AttackToTarget(GameObject target){
        weaponL.Shoot(target.transform,weaponL.transform);
        weaponR.Shoot(target.transform,weaponR.transform);
    }
    public void ChangeAttackTarget(GameObject target){
        if(shootTargetList.Count==0){
            Debug.LogWarning("场上没有敌人");
            return;
        }
        if(this.shootTarget==target){
            IErandom = StartCoroutine(RandomTarget(target));
        }
    }
    public void EndChangeAttackTarget(GameObject target){
        if(this.shootTarget!=target){
            StopCoroutine(IErandom);
        }
    }

    public bool IsTargetInArea(){
        Collider[] colliders = Physics.OverlapSphere(transform.position,attckRadius,1<<3);
        if(colliders.Length<=0)return false;
        return true;
    }
    //在玩家周围
    public bool IsPlayerInArea(){
         Collider[] colliders = Physics.OverlapSphere(transform.position,attckRadius,1<<6);
        if(colliders.Length<=0)return false;
        return true;
    }


    //随机下一个下标
    IEnumerator RandomTarget(GameObject target){
        while(this.shootTarget==target){
            this.shootTarget = shootTargetList[Random.Range(0,shootTargetList.Count-1)];
            yield return null;
        }
    }


    //追踪目标
    public void ChaseTarget(Transform target){
        //计算偏移
        Vector3 offset = (target.position-transform.position).normalized;
        //计算角度差
        float angle = Vector3.Angle(transform.forward,offset);
        //转向目标时间
        float needTime = angle/(maxRotSpeed*(curSpeed/maxAccSpeed));
        if(needTime<0.001f){
            transform.forward = offset;
        }else{
            transform.forward = Vector3.Lerp(transform.forward,offset,Time.deltaTime/needTime).normalized;
        }

        if(Vector3.Distance(target.position,transform.position)>=attckRadius+accRadius){
            curSpeed+=Time.deltaTime*accSpeed;
            curSpeed = Mathf.Clamp(curSpeed,0,maxAccSpeed);
        }else if(Vector3.Distance(target.position,transform.position)>=attckRadius){
            curSpeed +=Time.deltaTime*accSpeed*0.05f;
            curSpeed = Mathf.Clamp(curSpeed,maxAccSpeed*0.5f,maxAccSpeed*0.7f);
        }else if(Vector3.Dot(Vector3.forward,target.position-transform.position)>=0){
            curSpeed -=Time.deltaTime*accSpeed*0.05f;
            curSpeed = Mathf.Clamp(curSpeed,maxAccSpeed*0.4f,maxAccSpeed*0.5f);
        }else if(Vector3.Dot(Vector3.forward,target.position-transform.position)<0){
            curSpeed += Time.deltaTime*accSpeed*10;
            curSpeed = Mathf.Clamp(curSpeed,maxAccSpeed*0.5f,maxAccSpeed);
        }
        transform.position += transform.forward*curSpeed*Time.deltaTime;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,attckRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,attckRadius+accRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(weaponL.transform.position,shootTarget.transform.position);
    }

}
