using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LEVEL_STATE
{
    COMPLETED,
    GAME_OVER
}
public class LevelTracker : MonoBehaviour
{
    [SerializeField] LevelData levelData;
    [SerializeField] float progressTimeRate = 30f;
    [SerializeField] float progressPerSplit = 0.2f;
    EnemySpawnManager enemySM;

    private float levelTime;
    private int eventIndexer;
    private bool isPaused=false;
    public float TotalTime
    {
        get
        {
            return levelTime;
        }
    }
    public float Progress
    {
        get
        {
            //Increase difficulty of enemies over time
            return 1f + levelTime / progressTimeRate * progressPerSplit;
        }
    }
    public static LevelTracker Instance { get; private set; }
    public delegate void CurrentLevelState(LEVEL_STATE currentlevelState);
    public static CurrentLevelState OnStateChange;
    private void OnEnable()
    {
        GameManager.OnStateChange += GameState;
    }
    private void OnDisable()
    {
        GameManager.OnStateChange -= GameState;
    }

    private void GameState(GAME_STATE currentgameState)
    {
        switch (currentgameState)
        {
            case GAME_STATE.STARTED:
                break;
            case GAME_STATE.PAUSED:
                {
                    isPaused = true;
                    break;
                }
            case GAME_STATE.RUNNING:
                {
                    isPaused = false;
                    break;
                }
        }
    }

    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        enemySM = EnemySpawnManager.Instance;
        PlayScreenPanelController.Instance.UpdateTimer(levelTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused) return;
        levelTime += Time.deltaTime;
        PlayScreenPanelController.Instance.UpdateTimer(levelTime);
        EventManager();
    }

    private void EventManager()
    {
        if (eventIndexer >= levelData.stageEvents.Count) return;
        if(levelTime>levelData.stageEvents[eventIndexer].time)
        {
            PlayScreenPanelController.Instance.ObjText(levelData.stageEvents[eventIndexer].message);
            //Debug.Log(levelData.stageEvents[eventIndexer].message);
            switch (levelData.stageEvents[eventIndexer].eventType)
            {
                case StageEventType.SpawnEnemy:
                    SpawnEnemies();
                    break;
                case StageEventType.SpawnBoss:
                    break;
                case StageEventType.WinStage:
                    WinStage();
                    break;
            }
           
            eventIndexer++;
        }
    }

    private void WinStage()
    {
        OnStateChange?.Invoke(LEVEL_STATE.COMPLETED);
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < levelData.stageEvents[eventIndexer].count; i++)
        {
            enemySM.SpawnEnemies(levelData.stageEvents[eventIndexer].enemyToSpawn);
        }
    }
}
