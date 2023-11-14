using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class WeaponStats
{
    public int damage;
    public float timeToAttack;
    public int numberOfAttacks;
    public WeaponStats(int damage,float timeToAttack,int numberOfAttacks)
    {
        this.damage = damage;
        this.timeToAttack = timeToAttack;
        this.numberOfAttacks = numberOfAttacks;
    }

    public void UpgradeWeaponStats(WeaponStats weaponUpgradeStats)
    {
        this.damage += weaponUpgradeStats.damage;
        this.timeToAttack +=weaponUpgradeStats.timeToAttack;
        this.numberOfAttacks += weaponUpgradeStats.numberOfAttacks;
      
    }
}

[CreateAssetMenu(fileName = "Data", menuName = "Data/WeaponData", order = 4)]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public WeaponStats stats;
    public GameObject weaponBasePrefab;
    public List<UpgradeData> upgrades;
}
