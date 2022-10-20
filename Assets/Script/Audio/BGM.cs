using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    private static BGM instance;

    public static BGM Instance {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BGM>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }
 
    void Awake()
    {
 
        //此脚本永不消毁，并且每次进入初始场景时进行判断，若存在重复的则销毁
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (this != instance)
        {
            Destroy(gameObject);
        }
 
    }
}
