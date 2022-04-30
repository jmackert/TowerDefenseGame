using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    private float speed = 25f;

    public void Seek(Transform _target){
        target = _target;
    }
    private void HitTarget(){
        Destroy(gameObject);
        Destroy(target.gameObject);
        WaveSpawner.numEnemiesAlive--;
    }
    private void DamageTarget(Transform enemy){

    }

    void Update(){
        if(target == null){
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame){
            HitTarget();
            return;
        }
        transform.LookAt(target.position, direction);
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }
}
