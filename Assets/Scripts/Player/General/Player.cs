using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Creature {

	public static Player Instance { get { return PlayModeManager.Player; } }

    public new PlayerData Data { get { return (PlayerData)base.Data; } }
}
