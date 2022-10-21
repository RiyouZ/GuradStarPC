using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendManager : Sigleton<FriendManager>
{
    public List<CharacterStats> friends = new List<CharacterStats>();
    public List<GameObject> friendBoats = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        StartOnEnable();
    }

    public void RegisterFriend(GameObject boat,CharacterStats value){
        friendBoats.Add(boat);
        friends.Add(value);
    }

    public void DeRegisterFriend(GameObject boat,CharacterStats value){
        friendBoats.Remove(boat);
        friends.Remove(value);
    }
    
    public void AddTargetList(GameObject target){
        Debug.Log("AddInManager"+target.name);
        foreach(var boat in friendBoats){
            boat.GetComponent<FriendBoat>().shootTargetList.Add(target);
            Debug.Log(boat);
        }
    }
    public void RemoveTargetList(GameObject target){
        Debug.Log("AddInManager"+target.name);
        foreach(var boat in friendBoats){
            boat.GetComponent<FriendBoat>().shootTargetList.Remove(target);
            Debug.Log(boat);
        }
    }

    public void StartOnEnable(){
        foreach(var boat in friendBoats){
            if(boat.GetComponent<FriendBoat>().enabled==false){
                boat.GetComponent<FriendBoat>().enabled = true;
            }
        }
    }

}
