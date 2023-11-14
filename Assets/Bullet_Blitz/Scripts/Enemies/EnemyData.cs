using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/EnemyData", order = 3)]
public class EnemyData : ScriptableObject
{
    public string Name;
    public GameObject enemyPrefab;
    public EnemyStats stats;
   
}
