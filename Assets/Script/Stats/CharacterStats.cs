using UnityEngine;

public class CharacterStats:MonoBehaviour
{
    public CharacterSO stats;


    #region:赋值数值
    public float CurSpeed{
        set{
            stats.curSpeed = Mathf.Clamp(value,0,stats.maxSpeed);
        }
        get{
            return stats.curSpeed;
        }
    }
    public float MaxSpeed{
        set{
            stats.maxSpeed = value;
        }
        get{
            return stats.maxSpeed;
        }
    }
    public float AccSpeed{
        set{
            stats.accSpeed = value;
        }
        get{
            return stats.accSpeed;
        }
    }
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
    public float CurOil{
        set{
            stats.curOil = Mathf.Clamp(value,0,stats.maxOil);
        }
        get{
            return stats.maxSpeed;
        }
    }
    public float MaxOil{
        set{
            stats.maxOil = value;
        }
        get{
            return stats.maxOil;
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

    public bool IsUnpower{
        set{
            if(stats.curOil<=0)
                stats.isUnpower = true;
            else
                stats.isUnpower = false;
        }
        get{
            return stats;
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
