using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EBState{
    Idle,
    Move,
    Attack,
    Chase,
    Dead
}


public class EnemyBoat : MonoBehaviour
{

    public Rigidbody rb;

    public EnemyWeapon weaponL;
    public EnemyWeapon weaponR;
    public CharacterStats state;

    //随机掉落物列表
    public List<GameObject> suppliesList = new List<GameObject>();
    //攻击目标对象列表
    public List<GameObject> shootTargetList = new List<GameObject>();
    //攻击玩家
    public GameObject player;
    //正在攻击的目标
    public GameObject shootTarget;
    //状态机
    public StateMachine<EnemyBoat> machine;
    //存储状态
    public Dictionary<EBState,_State<EnemyBoat>> stateDic;

    [Header("AI属性")]
    //攻击检测范围
    [Tooltip("攻击范围")]
    public float attckRadius;
    [Tooltip("追击范围")]
    public float accRadius;
    [Tooltip("加速度")]
    public float accSpeed;
    [Tooltip("最大速度")]
    public float maxAccSpeed;
    public float rotSpeed;
    [Tooltip("旋转速度")]
    public float maxRotSpeed;
    public float curSpeed;
    public float preSpeed = 0;
    [Tooltip("攻击范围外的加速度的百分比")]
    public float OutAttackAreaMultiSpeed = 0.05f;
    [Tooltip("攻击范围外的最大速度的百分比")]
    public float OutAttackAreaMaxMultiSpeed = 0.7f;
    [Tooltip("攻击范围内的目标在前方时减速度的百分比")]
    public float InForwardAttackAreaMultiSpeed = 0.05f;
    [Tooltip("攻击范围内的目标在前方时最大速度的百分比")]
    public float InForwardAttackAreaMaxMultiSpeed = 0.05f;
    [Tooltip("攻击范围内的目标在后方时加速度的百分比")]
    public float InBackAttackAreaMultiSpeed = 10f;
    [Tooltip("攻击范围内的目标在后方时最大速度的百分比")]
    public float InBackAttackAreaMaxMultiSpeed = 0.05f;
    [Tooltip("攻击范围内最小速度的百分比")]
    public float InAttackAreaMinMultiSpeed = 0.4f;



    private static Coroutine IErandom;
    protected void Awake(){
        state = GetComponent<CharacterStats>();
        //rb = GetComponent<Rigidbody>();
        weaponL = transform.Find("WeaponEL").GetComponent<EnemyWeapon>();
        weaponR = transform.Find("WeaponER").GetComponent<EnemyWeapon>();
        EnemyManager.Instance.RegisterEnemy(this,state);
    }

    protected  void OnEnable(){
        //base.OnEnable();
        state.CurHealth = state.MaxHealth;
        FriendManager.Instance.AddTargetList(this.gameObject);
        this.player = GameObject.FindObjectOfType<Boat>().gameObject;
        if(machine!=null)machine.SetCurState(stateDic[EBState.Chase]);
    }

    protected void Start(){
        //创建字典索引
        stateDic = new Dictionary<EBState, _State<EnemyBoat>>();
        stateDic.Add(EBState.Move,new EB_MOVE());
        stateDic.Add(EBState.Attack,new EB_ATTACK());
        stateDic.Add(EBState.Chase,new EB_CHASE());
        stateDic.Add(EBState.Dead,new EB_DEAD());
        stateDic.Add(EBState.Idle,new EB_IDLE());
        machine = new StateMachine<EnemyBoat>(this);
        machine.SetCurState(stateDic[EBState.Chase]);
    }
    protected void Update()
    {
        machine.OnUpdate();
        if(state.CurHealth<=0){
            ChangeState(EBState.Dead);
            //Destroy(gameObject);
        }
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    private void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.CompareTag("FX")){
            

        }

    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("EdgeZone")){
            shootTarget = player;
        }
    }

    /// <summary>
    /// OnCollisionExit is called when this collider/rigidbody has
    /// stopped touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    private void OnCollisionExit(Collision other)
    {
        if(!other.gameObject.CompareTag("FX")){
           
        }
    }


    //切换状态
    public void ChangeState(EBState ebState){
        if(!stateDic.ContainsKey(ebState))return;
        machine.ChangeCurState(stateDic[ebState]);
    }

    //移动
    public void Movement(float speed){
        rb.AddForce(transform.forward*speed*Time.deltaTime);
    }

    //转向
    public void RotateToTarget(Quaternion target,float speed){
        transform.localRotation = Quaternion.Lerp(transform.localRotation,target,Time.deltaTime*speed);
    }

    //检测范围
    public bool IsTargetInArea(){
        Collider[] colliders = Physics.OverlapSphere(transform.position,attckRadius,(1<<6)|(1<<12));
        if(colliders.Length<=0)return false;
        return true;
    }

    //攻击目标
    public void AttackToTarget(GameObject target){
        weaponL.Shoot(target.transform,weaponL.transform);
        weaponR.Shoot(target.transform,weaponR.transform);
    }

    //切换攻击目标
    public void ChangeAttackTarget(GameObject target){
        if(this.shootTarget==target){
            IErandom = StartCoroutine(RandomTarget(target));
        }
    }
    public void EndChangeAttackTarget(GameObject target){
        if(this.shootTarget!=target){
            StopCoroutine(IErandom);
        }
    }
     IEnumerator RandomTarget(GameObject target){
        while(this.shootTarget==target){
            this.shootTarget = shootTargetList[Random.Range(0,shootTargetList.Count-1)];
            yield return null;
        }
    }
    //检测碰撞
    public bool IsCollEnter(Transform target){
        RaycastHit hit;
        Vector3 direction = target.position-transform.position;
        Ray ray = new Ray(transform.position,direction);
        if(Physics.Raycast(ray,out hit)){
            float dist = Vector3.Distance(transform.position,hit.point);
            if(dist<=20){
                return true;
            }
            else{
                return false;
            }
        }
        return false;
    }

    //获取碰撞时的法线
    public Vector3 GetCollNormal(Transform target){
        RaycastHit hit;
        Vector3 direction = target.position-transform.position;
        Ray ray = new Ray(transform.position,direction);
        if(Physics.Raycast(ray,out hit)){
            return hit.normal;
        }
        return Vector3.zero;
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
            curSpeed +=Time.deltaTime*accSpeed*OutAttackAreaMultiSpeed;
            curSpeed = Mathf.Clamp(curSpeed,maxAccSpeed*InForwardAttackAreaMaxMultiSpeed,maxAccSpeed*OutAttackAreaMaxMultiSpeed);
        }else if(Vector3.Dot(Vector3.forward,target.position-transform.position)>=0){
            curSpeed -=Time.deltaTime*accSpeed*InForwardAttackAreaMultiSpeed;
            curSpeed = Mathf.Clamp(curSpeed,maxAccSpeed*InAttackAreaMinMultiSpeed,maxAccSpeed*InForwardAttackAreaMaxMultiSpeed);
        }else if(Vector3.Dot(Vector3.forward,target.position-transform.position)<0){
            curSpeed += Time.deltaTime*accSpeed*InBackAttackAreaMultiSpeed;
            curSpeed = Mathf.Clamp(curSpeed,maxAccSpeed*InAttackAreaMinMultiSpeed,maxAccSpeed*InBackAttackAreaMaxMultiSpeed);
        }
        transform.position += transform.forward*curSpeed*Time.deltaTime;

    }

    //注册目标列表
    public void AddTargetList(GameObject target){
        Debug.Log("Add"+target.name);
        shootTargetList.Add(target);
    }

    //Debug
    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
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
