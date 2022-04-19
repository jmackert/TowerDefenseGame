using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    private float speed = 70f;

    public void Seek(Transform _target){
        target = _target;
    }
    private void HitTarget(){
        Destroy(gameObject);
        Destroy(target.gameObject);
        WaveSpawner.numEnemiesAlive--;
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
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }
}
