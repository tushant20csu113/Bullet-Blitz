using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SkillUpgrade
{
     public int health;
     public int attack;
     [Range(0,10)] public int hpRegenerationRate;
}
[CreateAssetMenu(fileName = "Data", menuName = "Data/SkillData", order = 4)]
public class SkillData : ScriptableObject
{
    [Header("Adds Skill Stats")]
    public string skillName;
    public SkillUpgrade stats;
    public List<UpgradeData> upgrades;
}
