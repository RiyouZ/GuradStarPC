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


public class EnemyBoat : Enemy
{

    public Rigidbody rb;

    public EnemyWeapon weaponL;
    public EnemyWeapon weaponR;

    public BoatStats boatState;


    //随机掉落物列表
    public List<string> suppliesList;
    //攻击目标对象列表
    public List<GameObject> shootTargetList = new List<GameObject>();
    //正在攻击的目标
    public GameObject shootTarget;
    //状态机
    public StateMachine<EnemyBoat> machine;
    //存储状态
    public Dictionary<EBState,_State<EnemyBoat>> stateDic;

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


    protected override void Awake(){
        base.Awake();
        EnemyManager.Instance.RegisterEnemy(this,state);
    }

    protected override void OnEnable(){
        //base.OnEnable();
    }

    protected override void Start(){
        base.Start();
        rb = GetComponent<Rigidbody>();
        boatState = GetComponent<BoatStats>();
        weaponL = GameObject.Find("WeaponEL").GetComponent<EnemyWeapon>();
        weaponR = GameObject.Find("WeaponER").GetComponent<EnemyWeapon>();
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
    protected override void Update()
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
        Collider[] colliders = Physics.OverlapSphere(transform.position,attckRadius,1<<6);
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
        if(this.shootTarget==target)return;
        this.shootTarget = target;
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
            curSpeed +=Time.deltaTime*accSpeed*0.05f;
            curSpeed = Mathf.Clamp(curSpeed,2,maxAccSpeed*0.7f);
        }else{
            curSpeed-=Time.deltaTime*accSpeed;
            curSpeed = Mathf.Clamp(curSpeed,2,maxAccSpeed);
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
