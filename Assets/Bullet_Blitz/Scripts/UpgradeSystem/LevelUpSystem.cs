using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpSystem : MonoBehaviour
{
    private int level = 1;
    private int experience = 0;
    private int expNeededToLevelUp = 1000;//Debug Variable

    [SerializeField] List<UpgradeData> upgrades;
    List<UpgradeData> selectedUpgrades;
    [SerializeField] List<UpgradeData> acquireUpgrades;
    //Event for leveling up
    public delegate void LevelUp();
    public static event LevelUp OnLevelUp;
   
    //Getters functions
    public int TO_LEVEL_UP
    {
        get
        {
            //returns exp needed to level up
            return level * 1000;
        }
    }
    public int EXPERIENCE
    {
        get
        {
            return experience;
        }
    }
    public int LEVEL
    {
        get
        {
            return level;
        }
    }
    //Singleton
    public static LevelUpSystem Instance { get; private set; }
    //Event subscribing and unsubscribe
    private void OnEnable()
    {
        PickUp.OnPickupExp += OnEXPPickup;
    }
    private void OnDisable()
    {
        PickUp.OnPickupExp -= OnEXPPickup;
    }
    //Adds experience on EXP pick up
    private void OnEXPPickup(ObjectType objType, int amount)
    {
        if (objType == ObjectType.EXP)
            AddExperience(amount);
    }

    private void Awake()
    {
       if (Instance == null)
       {
           Instance = this;
       }
       else
           Destroy(Instance);
    }
    
   
    
    public void AddExperience(int expAmount)
    {
        experience += expAmount;
        CheckLevelUp();
        expNeededToLevelUp = TO_LEVEL_UP;

    }
    /// <summary>
    /// Adds Weapon upgrades to list of available upgrades
    /// </summary>
    /// <param name="upgrades"></param>
    public void AddWeaponsUpgrades(UpgradeData upgradesToAdd)
    {
        this.upgrades.Add(upgradesToAdd);
    }
    private void CheckLevelUp()
    {
        if(experience>= TO_LEVEL_UP)
        {
            //Experience Calculation
            experience -= TO_LEVEL_UP;
          
            level += 1;
            GameManager.OnStateChange?.Invoke(GAME_STATE.PAUSED);
            OnLevelUp?.Invoke();//Event Invoked
            //Initialise selected upgrades list
            if (selectedUpgrades == null) { selectedUpgrades = new List<UpgradeData>(); }
            selectedUpgrades.Clear();
            selectedUpgrades.AddRange(GetUpgrades(4));
            HUDManager.Instance.OpenUpgradePanel(selectedUpgrades);
            
        }
    }
    /// <summary>
    /// Manages list of acquired and available upgrades
    /// </summary>
    /// <param name="selectedUpgradeId"></param>
    public void Upgrade(int selectedUpgradeId)
    {
        UpgradeData upgradeData = selectedUpgrades[selectedUpgradeId];
        if (acquireUpgrades == null) { acquireUpgrades = new List<UpgradeData>(); }
        switch (upgradeData.upgradeType)
        {
            case UpgradeType.WeaponUpgrade:
                {
                    WeaponManager.Instance.UpgradeWeapon(upgradeData);
                }
                break;
            case UpgradeType.StatUnlock:
                {               
                    SkillManager.Instance.AddSkill(upgradeData.skillData);
                }
                break;
            case UpgradeType.WeaponUnlock:
                {
                    WeaponManager.Instance.AddWeapon(upgradeData.weaponData);
                }
                break;
            case UpgradeType.StatUpgrade:
                {
                    SkillManager.Instance.UpgradeSkill(upgradeData);
                }
                break;
            default:
                break;
        }
        //Add selected upgrade to acquired upgrades
        acquireUpgrades.Add(upgradeData);
        //Remove selected upgrade from available upgrades
        upgrades.Remove(upgradeData);
        GameManager.OnStateChange?.Invoke(GAME_STATE.RUNNING);
 
    }

    /// <summary>
    /// Returns a list of random available upgrades
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    public List<UpgradeData> GetUpgrades(int count)
    {
        List<UpgradeData> upgradeList = new List<UpgradeData>();
        if(count> upgrades.Count)
        {
            count = upgrades.Count;
        }
        
        upgradeList.AddRange(RandomsUpgrades(upgrades,count));
              

        return upgradeList;
    }
    /// <summary>
    /// Returns a list of random and unique upgrades
    /// </summary>
    /// <param name="clone"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    private List<UpgradeData> RandomsUpgrades(List<UpgradeData> clone,int count)
    {
        
        for (int i = 0; i < count-1; i++)
        {
            UpgradeData temp = clone[i];
            int randomIndex = Random.Range(i, clone.Count);
            clone[i] = clone[randomIndex];
            clone[randomIndex] = temp;
        }
        return clone;
    }
}
