using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance { get; private set; }
    private int count = 0;
    SkillData skillData;
    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            
        }
       
    }
    public void AddSkill(SkillData skillData)
    {
        this.skillData = skillData;
        SetData(skillData.stats);
        LevelUpSystem.Instance.AddWeaponsUpgrades(skillData.upgrades[0]);
    }
    public void UpgradeSkill(UpgradeData upgradeData)
    {
        if (count >= skillData.upgrades.Count) return;
        SetData(skillData.upgrades[count].skillData.stats);
        LevelUpSystem.Instance.AddWeaponsUpgrades(skillData.upgrades[count]);
        count++;
    }
    void SetData(SkillUpgrade stats)
    {
   
        PlayerStats.Instance.MAX_HP += stats.health;
        PlayerStats.Instance.CURRENT_HP += stats.health;
        PlayerStats.Instance.HP_REGENERATION_RATE = stats.hpRegenerationRate;
    }
   
}
