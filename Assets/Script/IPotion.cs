using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PotionTypes
{
    Q,
    R,
    U,
    P,
}

public interface IPotion{

    void Spawn(Vector3 spawnPosition,PotionTypes type, float speed);
    void Refill();
    bool isRefilled();
    void WrongLiquid();
    bool isWrong();
    PotionState CurrentState { get; set; }
    PotionTypes CurrentType { get; set; }
    PotionTypes RefillerZone { get; set; }
    bool InRefillerZone { get; set; }
    GameObject gameObject { get; }
}
