using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuppliesStats : MonoBehaviour
{
    public SuppliesSO stats;
    public SuppliesSO tmpStats;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        if(tmpStats!=null){
            stats = Instantiate(tmpStats);
        }
    }

    #region :赋值访问器
    public float RecoverValue{
        set{
            stats.recoverValue = Mathf.Max(0,value);
        }
        get{
            return stats.recoverValue;
        }
    }

    public float CurLifeTime{
        set{
            stats.curLifeTime = value;
            stats.curLifeTime = Mathf.Clamp(value,0,stats.maxLifeTime);
        }
        get{
            return stats.curLifeTime;
        }
    }
    public float MaxLifeTime{
        set{
            stats.maxLifeTime = Mathf.Max(0,value);
        }
        get{
            return stats.maxLifeTime;
        }

    }
    #endregion

    //恢复油量
    public void Recover(BoatStats boat){
        boat.CurOil+=RecoverValue;
    }

    //恢复血量
    public void Recover(CharacterStats character){
        character.CurHealth+=(int)RecoverValue;
    }

}