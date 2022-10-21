using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatStats:MonoBehaviour
{
    public BoatSO stats;
    public BoatSO tmpStats;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        if(tmpStats)stats = Instantiate(tmpStats);
    }

    #region :数值赋值

    public float RotSpeed{
        set{
            stats.rotSpeed = value;
        }
        get{
            return stats.rotSpeed;
        }
    }

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

    public float BrakeSpeed{
        set{
            stats.brakeSpeed = Mathf.Max(0,stats.curSpeed*stats.MultiBrakeSpeed);
        }
        get{
            return stats.brakeSpeed;
        }
    }


    public float CurOil{
        set{
            stats.curOil = Mathf.Clamp(value,0,stats.maxOil);
        }
        get{
            return stats.curOil;
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

    public bool IsUp{
        set{
            stats.isUp = value;
        }
        get{
            return stats.isUp;
        }
    }
    public bool IsDown{
        set{
            stats.isDown = value;
        }
        get{
            return stats.isDown;
        }
    }
    public bool IsLeft{
        set{
            stats.isLeft = value;
        }
        get{
            return stats.isLeft;
        }
    }
    public bool IsRight{
        set{
            stats.isRight = value;
        }
        get{
            return stats.isRight;
        }
    }
    public bool IsForward{
        set{
            stats.isForward = value;
        }
        get{
            return stats.isForward;
        }
    }
    public bool IsBrake{
        set{
            stats.isBrake = value;
        }
        get{
            return stats.isBrake;
        }
    }
    public bool IsUpAndLeft{
        set{
            stats.isUpAndLeft = value;
        }
        get{
            return stats.isUpAndLeft;
        }
    }

    public bool IsUpAndRight{
        set{
            stats.isUpAndRight = value;
        }
        get{
            return stats.isUpAndRight;
        }
    }

    public bool IsDownAndLeft{
        set{
            stats.isDownAndLeft = value;
        }
        get{
            return stats.isDownAndLeft;
        }
    }
    public bool IsDownAndRight{
        set{
            stats.isDownAndRight = value;
        }
        get{
            return stats.isDownAndRight;
        }
    }




    #endregion

    #region :计算方法
    //耗油
    public void ConsumeOil(float spend){
        CurOil-=spend;
    }


    #endregion

}

