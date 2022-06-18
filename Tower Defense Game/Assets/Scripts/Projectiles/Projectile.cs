using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Transform target;
    protected float speed;
    protected float damageAmount;

    public void Seek(Transform _target){
        target = _target;
    }
    private void DamageTarget(){
        IDamageable<float> damageable = target.GetComponent<IDamageable<float>>();
        if(damageable != null){
            damageable.TakeDamage(damageAmount);
        }
    }
    private void Disable(){
        gameObject.SetActive(false);
    }
    void Update(){
        if(target == null){
            Disable();
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame){
            DamageTarget();
            Disable();
            return;
        }
        transform.LookAt(target.position, direction);
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }
}
