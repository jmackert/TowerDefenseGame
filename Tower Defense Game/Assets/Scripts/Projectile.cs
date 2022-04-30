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
    private void DamageTarget(){
        IDamageable<float> damageable = target.GetComponent<IDamageable<float>>();
        if(damageable != null){
            damageable.TakeDamage(50f);
        }
    }

    void Update(){
        if(target == null){
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame){
            //HitTarget();
            DamageTarget();
            return;
        }
        transform.LookAt(target.position, direction);
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }
}
