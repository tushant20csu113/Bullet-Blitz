using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PROJECTILE
{
    BULLET,
    MISSILE
}
[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private PROJECTILE projectileType;
    [HideInInspector]  public int damage = 50;
    [HideInInspector]  public float range=20f;
    [HideInInspector] public float speed=7f;
    [SerializeField] float damageRadius = 3f;
    [SerializeField]  LayerMask collidable;
    Vector2 direction;
   
    Rigidbody2D _rb;
    

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
 
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.green;
        switch (projectileType)
        {
            case PROJECTILE.BULLET:
                {
                    //UnityEditor.Handles.DrawLine(transform.position,direction);
                 break;
                }
            case PROJECTILE.MISSILE:
                {

                     UnityEditor.Handles.DrawWireDisc(transform.position - transform.right, Vector3.forward, damageRadius);
                break;
                }
        }
    }
#endif
    public void SetDirectionAndRotation(float dirX,float dirY)
    {
        direction = new Vector2(dirX, dirY);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);

    }
    private void FixedUpdate()
    {
        switch (projectileType)
        {
            case PROJECTILE.BULLET:
                {
                    BulletDamageDetection();
                    MoveBullet();
                    break;
                }

            case PROJECTILE.MISSILE:
                {
                    MissileDamageDetection();
                    MoveMissile();
                    break;
                }
        }
    }
    
    void MoveBullet()
    {
       // _rb.AddForce(direction * speed * Time.deltaTime, ForceMode2D.Impulse);
       // transform.Translate(direction * speed * Time.deltaTime);
        _rb.MovePosition(_rb.position + direction * speed * Time.fixedDeltaTime);
        // transform.position += direction * speed * Time.deltaTime;
    }
    void MoveMissile()
    {
       // float force = 5f;
        _rb.MovePosition(_rb.position + direction * speed * Time.fixedDeltaTime);
       // _rb.AddForce(direction * force * Time.deltaTime, ForceMode2D.Impulse);
    }
    private void BulletDamageDetection()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position,direction,0.1f,collidable);
        Debug.DrawRay(transform.position, direction, Color.green);
        if (hit.collider != null)
        {
            IDamageable damageable = hit.transform.GetComponent<IDamageable>();
            if (damageable != null)
            {

                PostDamage(damage, transform.position);
                damageable.TakeDamage(damage);
            }
            DiscardToPool();
            
        }
    
    }

  
    public void MissileDamageDetection()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position- transform.right, damageRadius,collidable);
        if (hit == null) return;
        foreach (Collider2D c in hit)
        {
            IDamageable damageable = c.GetComponent<IDamageable>();
            if (damageable != null)
            {
               // hitDetected = true;
                PostDamage(damage, transform.position);
                damageable.TakeDamage(damage);
            
            }
            DiscardToPool();
            
        }
     
        
    }
    public IEnumerator TimeToLive(float destroytime)
    {
        yield return new WaitForSeconds(destroytime);
        // this.gameObject.SetActive(false);
        DiscardToPool();


    }
    private void PostDamage(int damage,Vector3 worldPosition)
    {
        MessageSystem.Instance.PostMessage(damage.ToString(), worldPosition);
    }

    public void OnRequestedFromPool()
    {
        _rb.velocity = Vector2.zero;
    }

    public void DiscardToPool()
    {
        _rb.velocity = Vector2.zero;
        ObjectPooler.Destroy(this.gameObject);
       // MyPooler.ObjectPooler.Instance.ReturnToPool(this.gameObject.name, this.gameObject);
    }
}
