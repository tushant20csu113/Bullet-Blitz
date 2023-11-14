using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MissileWeapon : WeaponBase
{
    [SerializeField] GameObject missilePrefab;
    [SerializeField] float range = 10f;
    public override void Attack()
    {
          Deploy();
          //Invoke("Deploy",0.5f);   
    }
     void Deploy()
    {
            for (int i = 0; i < weaponStats.numberOfAttacks; i++)
            {
                //GameObject firedMissile = MyPooler.ObjectPooler.Instance.GetFromPool("Missile", transform.position, Quaternion.identity);
             //   GameObject firedMissile = Instantiate(missilePrefab, transform.position, Quaternion.identity);
                GameObject firedMissile =ObjectPooler.Generate("Missile", transform.position, Quaternion.identity);
                //firedMissile.SetActive(true);
                //GameObject firedMissile = ObjectPooler.Generate(missilePrefab, transform.position, Quaternion.identity);
                firedMissile.name = "Missile";
                firedMissile.transform.position = transform.position;
                Vector2 direction = ClosestEnemyDirection();

                Projectile firedMissileProjectile = firedMissile.GetComponent<Projectile>();
                firedMissileProjectile.damage = weaponStats.damage;
                firedMissileProjectile.speed = 5f;
                firedMissileProjectile.SetDirectionAndRotation(direction.x, direction.y);
                StartCoroutine(firedMissileProjectile.TimeToLive(range / 5f));
        }
    }
    public Vector2 RandomDirection()
    {
        float xPos = Random.Range(-1f, 1f);
        float yPos = Random.Range(-1f, 1f);
        
        Vector2 randomPos = new Vector2(xPos, yPos);
        //Debug.Log("Random Position generated: "+randomPos);
        return randomPos;
       
    
    }
    public Vector2 ClosestEnemyDirection()
    {
        LayerMask EnemyLayer =LayerMask.GetMask("Enemy");
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position , 5f, EnemyLayer);
        if(hit==null||hit.Length==0)
        {
           // Debug.Log("Random Direction");
            return RandomDirection();
        }
        int ranNum = Random.Range(0,hit.Length-1);
        Vector3 closestEnemyPos = hit[ranNum].transform.position;
        //Debug.Log("Random Enemy");
        return (closestEnemyPos-transform.position).normalized;
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, 3);
    }
#endif


}
