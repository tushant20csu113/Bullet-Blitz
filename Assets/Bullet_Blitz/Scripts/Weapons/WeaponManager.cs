using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] Transform weaponObjectsContainer;
    //[SerializeField] WeaponData startingWeapon;
    [SerializeField]List<GameObject> guns;
    //List of active weapons
     List<WeaponBase> weapons;
    WeaponData wd;
    private int count=0;

    public static WeaponManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(Instance);
        weapons = new List<WeaponBase>();
    }
    // Start is called before the first frame update
    void Start()
    {
        int ranNum = Random.Range((int)0, guns.Count);
        guns[ranNum].SetActive(true);
        //AddWeapon(startingWeapon);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            guns[0].SetActive(true);
            guns[1].SetActive(false);
            guns[2].SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            guns[0].SetActive(false);
            guns[1].SetActive(true);
            guns[2].SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            guns[0].SetActive(false);
            guns[1].SetActive(false);
            guns[2].SetActive(true);
        }
    }
    public void AddWeapon(WeaponData weaponData)
    {
        GameObject weaponPrefab = Instantiate(weaponData.weaponBasePrefab, weaponObjectsContainer);
        WeaponBase weaponBase = weaponPrefab.GetComponent<WeaponBase>();
        weaponBase.SetData(weaponData);
        weapons.Add(weaponBase);
        wd = weaponData;
        LevelUpSystem.Instance.AddWeaponsUpgrades(weaponData.upgrades[0]);
    }

    public void UpgradeWeapon(UpgradeData upgradeData)
    {
     
        WeaponBase weaponToUpgrade = weapons.Find(wd => wd.weaponData == upgradeData.weaponData);
        if (weaponToUpgrade != null)
        {
            weaponToUpgrade.Upgrade(upgradeData);
            if (count >= wd.upgrades.Count) return;
            LevelUpSystem.Instance.AddWeaponsUpgrades(wd.upgrades[count]);
            count++;
        }
        else
            Debug.Log("Number of attacks are " + upgradeData.weaponUpgradeStats.numberOfAttacks);
    }
}
