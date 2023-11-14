using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StageEventType
{
    SpawnEnemy,
    SpawnBoss,
    WinStage
}
[System.Serializable]
public class StageEvent
{
    public StageEventType eventType;
    public float time;
    public string message;
    public EnemyData enemyToSpawn;
    public int count;
}
[CreateAssetMenu(fileName = "Data", menuName = "Data/LevelData", order = 2)]
public class LevelData : ScriptableObject
{
    public List<StageEvent> stageEvents;
}
