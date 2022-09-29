using UnityEngine;

public class CharacterStats:MonoBehaviour
{
    public CharacterSO stats;


    #region:赋值数值
    
    public int CurHealth{
        set{
            stats.curHealth = Mathf.Clamp(value,0,stats.maxHealth);
        }
        get{
            return stats.curHealth;
        }
    }
    public int MaxHealth{
        set{
            stats.maxHealth = value;
        }
        get{
            return stats.maxHealth;
        }

    }
    public float HotTime{
        set{
            stats.hotTime = value;
        }
        get{
            return stats.hotTime;
        }

    }
    public float CurTime{
        set{
            stats.curTime = Mathf.Max(0,CurTime);
        }
        get{
            return stats.curTime;
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



}
