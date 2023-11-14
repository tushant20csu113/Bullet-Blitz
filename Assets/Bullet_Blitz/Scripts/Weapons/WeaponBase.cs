using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public WeaponData weaponData;

    protected WeaponStats weaponStats;
    protected float timetoAttack = 1f;
    float timer;

    private void OnEnable()
    {
        StartCoroutine(TimeToLive());
    }

    IEnumerator TimeToLive()
    {
        yield return new WaitForSeconds(timetoAttack);
        Attack();
        StartCoroutine(TimeToLive());
    }

    // Update is called once per frame
   /* void Update()
    {
        timer -= Time.deltaTime;
        if(timer<0f)
        {
            Attack();
            timer = timetoAttack;
        }    
    }*/
   
   //Setting stats for weapon when added to acquired upgrades list
    public virtual void SetData(WeaponData wd)
    {
        weaponData = wd;
        timetoAttack = weaponData.stats.timeToAttack;
        weaponStats = new WeaponStats(wd.stats.damage, wd.stats.timeToAttack,wd.stats.numberOfAttacks);
    }
    public abstract void Attack();
    //On upgrade change weapon stats
    public void Upgrade(UpgradeData upgradeData)
    {
        weaponStats.UpgradeWeaponStats(upgradeData.weaponUpgradeStats);
        timetoAttack += upgradeData.weaponUpgradeStats.timeToAttack;
        Debug.LogWarning(timetoAttack);

    }

}
