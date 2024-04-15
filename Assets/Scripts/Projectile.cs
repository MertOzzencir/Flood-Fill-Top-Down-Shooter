using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public LayerMask collisionMasks;
    public float speed = 35f;
    float damage = 1f;
    float lifeTime = 3f;
    float widghtSize = 0.1f;

    private void Start()
    {
        Destroy(gameObject,lifeTime);

        Collider[] initialColliders = Physics.OverlapSphere(transform.position, .3f, collisionMasks);

        if (initialColliders.Length > 0)
        {

            OnHitObject(initialColliders[0]);   
        }
    }
    private void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        CheckCollisions(moveDistance);
        transform.Translate(Vector3.forward * moveDistance);

        
    }

    public void SetSpeed(float objectSpeed)
    {
        speed = objectSpeed;

    }
    void CheckCollisions(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, moveDistance + widghtSize,collisionMasks,QueryTriggerInteraction.Collide))
        {

            OnHitObject(hit);
        }


    }

    void OnHitObject(RaycastHit hit)
    {
        
        IDamageable damageable = hit.collider.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeHit(damage, hit);

        }
        Destroy(gameObject);
    }

    void OnHitObject(Collider c)
    {

        IDamageable damageable = c.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);

        }
        Destroy(gameObject);
    }


}
