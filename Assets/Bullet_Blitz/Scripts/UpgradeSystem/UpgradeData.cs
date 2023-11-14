using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

public enum UpgradeType
{
    WeaponUpgrade,
    StatUnlock,
    WeaponUnlock,
    StatUpgrade
}
[CreateAssetMenu(fileName = "Data", menuName = "Data/UpgradeData", order = 1)]
public class UpgradeData : ScriptableObject
{
    public string Name;
    public Sprite icon;
    public string upgradeInfo;
    public UpgradeType upgradeType;
    public WeaponData weaponData;
    public WeaponStats weaponUpgradeStats;
    public SkillData skillData;
    public SkillUpgrade upgrades;

    //Add custom editor
    /* #if UNITY_EDITOR
         #region
         [CustomEditor(typeof(UpgradeData))]
         public class UpgradeDataEditor : Editor
         {
             public UpgradeData upgradeData;
             private void OnEnable()
             {
                  upgradeData = (UpgradeData)target;
             }
             public override void OnInspectorGUI()
             {
                 //base.OnInspectorGUI();

                 //DrawDetails(upgradeData);
                 switch (upgradeData.upgradeType)
                 {
                     case UpgradeType.WeaponUpgrade:
                         {
                             EditorGUILayout.PropertyField(serializedObject.FindProperty("weaponData"), new GUIContent("Weapon Data"));
                             serializedObject.ApplyModifiedProperties();
                             upgradeData.weaponData.Insert(upgradeData.weaponData, new WeaponData());
                             //ShowWeaponUpgrade(upgradeData);
                             break;
                         }
                     case UpgradeType.ItemUpgrade:
                         break;
                     case UpgradeType.WeaponUnlock:
                         break;
                     case UpgradeType.ItemUnlock:
                         break;
                     default:
                         break;
                 }
                 static void ShowWeaponUpgrade(UpgradeData upgradeData)
                 {
                     EditorGUILayout.Space();

                     //WeaponData weaponData = EditorGUILayout.ObjectField("Weapon Data", weaponData, typeof(WeaponData), false) as WeaponData;
                     upgradeData.showWeaponUpgradeStats = EditorGUILayout.Foldout(upgradeData.showWeaponUpgradeStats, "Upgrade Stats",true);


                 }
             }


         }
         #endregion
     #endif*/
}
