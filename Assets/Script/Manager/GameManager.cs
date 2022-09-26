using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Sigleton<GameManager>
{
    public BoatStats boat;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
    }


}
