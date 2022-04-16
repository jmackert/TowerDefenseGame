using UnityEngine;
public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 2.5f;
    private Transform target;
    private int waypointIndex = 0;
    [SerializeField]
    private float rotationSpeed = 6f;

    void Start() {
        target = Waypoints.waypoints[0];    
    }
    void Update() {
        Move();
        if (Vector3.Distance(transform.position, target.position) <= 0.2f){
            GetNexWaypoint();
        }
    }
    private void Move(){
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        transform.Translate(direction.normalized * movementSpeed * Time.deltaTime, Space.World); 
    }
    private void GetNexWaypoint(){
        if(waypointIndex >= Waypoints.waypoints.Length - 1){
            Destroy(gameObject);
            return;
        }
        waypointIndex++;
        target = Waypoints.waypoints[waypointIndex];
    }
}
