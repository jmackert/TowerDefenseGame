using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable<T>{
    void TakeDamage(T damageAmount);
}

public interface ISpawnable<T, J, I>{
    int GetWaypointIndex();
    void SetWaypointIndex(T newWaypointIndex, J newPreviousWaypoint, I newTarget);
}