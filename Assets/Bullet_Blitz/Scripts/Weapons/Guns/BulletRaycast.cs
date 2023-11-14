using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class BulletRaycast :MonoBehaviour
{
    
    public static void Shoot(Vector3 gunEndPoint, Vector3 target,int damage)
    {
        float radius = 3f;
        LayerMask EnemyLayer = LayerMask.GetMask("Enemy");
        Vector3 shootDirection = (target - gunEndPoint).normalized;
        RaycastHit2D raycasthit2D = Physics2D.CircleCast(gunEndPoint,radius, shootDirection, Mathf.Infinity, EnemyLayer);

       

        if (raycasthit2D.collider!=null)
        {
            GameEffects.Instance.ShootEffects(gunEndPoint, raycasthit2D.collider.transform.position);
            //On Hit
            Enemy hitTarget = raycasthit2D.collider.GetComponent<Enemy>();
            if(hitTarget != null)
            {
                hitTarget.TakeDamage(damage);
            }
        }
        else
        {
            GameEffects.Instance.ShootEffects(gunEndPoint, target);
        }
     
    }
   
}
