using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class BulletProjectile :MonoBehaviour
{
  
    int damage = 10;
    public static void Shoot(GameObject bulletPrefab,float shootForce,Vector3 gunEndPoint, Vector3 target)
    {
      
        float force = shootForce;
        Vector3 shootDirection = (target - gunEndPoint).normalized;
        GameObject bullet = Instantiate(bulletPrefab, gunEndPoint, Quaternion.identity);
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
        rbBullet.AddForce(shootDirection *force * Time.deltaTime, ForceMode2D.Impulse);
       

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Enemy"))
        {
            collision.transform.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
