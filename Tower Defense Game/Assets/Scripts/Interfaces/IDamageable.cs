using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable<T>{
    void TakeDamage(T damageAmount);
}

public interface ISpawnable<T,I>{
    int GetWaypointIndex();
    void SetWaypointIndex(T newWaypointIndex, I newTarget);
}