using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
    public delegate void PoolEvents(IPotion potion);
    public static PoolEvents OnPotionSpawn;
    public static PoolEvents OnPotionDestroy;

    public delegate void GameEvent();
    public static GameEvent GameOver;
}
