using UnityEngine;

public class CharacterStats:MonoBehaviour
{
    public CharacterSO stats;

    public CharacterSO tmpStats;

    public int socore;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        if(tmpStats)stats = Instantiate(tmpStats);
    }

    #region:赋值数值 
    
    public float CurHealth{
        set{
            stats.curHealth = Mathf.Clamp(value,0,stats.maxHealth);
        }
        get{
            return stats.curHealth;
        }
    }
    public float MaxHealth{
        set{
            stats.maxHealth = value;
        }
        get{
            return stats.maxHealth;
        }

    }
    public int AtkVal{
        set{
            stats.atkVal = value;
        }
        get{
            return stats.atkVal;
        }

    }
    public float CurHotTime{
        set{
            stats.curHotTime = Mathf.Clamp(value,0,stats.maxHotTime);
        }
        get{
            return stats.curHotTime;
        }

    }

    public float MaxHotTime{
        set{
            stats.maxHotTime = value;
        }
        get{
            return stats.maxHotTime;
        }
    }

    public float CurCoolTime{
        set{
            stats.curCoolTime = value;
            stats.curCoolTime = Mathf.Max(0,stats.curCoolTime);
        }
        get{
            return stats.curCoolTime;
        }
    }
    public float CoolTime{
        set{
            stats.coolTime = value;
        }
        get{
            return stats.coolTime;
        }
    }

    public bool IsShoot{
        set{
            stats.isShoot = value;
        }
        get{
            return stats.isShoot;
        }

    }

    public bool IsGrab{
        set{
            stats.isGrab = value;
        }
        get{
            return stats.isGrab;
        }
    }

    public bool isDead{
        set{
            if(stats.curHealth<=0)
                stats.isDead = true;
            else
                stats.isDead = false;
        }

        get{
            return stats.isDead;
        }

    }

    #endregion

    #region:数值计算方法
    //攻击计算

    public float TakeDamage(CharacterStats attack,CharacterStats target){
        target.CurHealth-=attack.AtkVal;
        return target.CurHealth;
    }



    #endregion


}
