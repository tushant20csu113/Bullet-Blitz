using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups;
        public int waveQuota;// The total number of enemies to spawn in this wave
        public float spawnInterval;// The interval at which to spawn enemies
        public int spawnedCount;
        
    }
    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount;// The  number of enemies to spawn in this wave
        public int spawnedCount;// The number of enemies of this type already spawned in this wave
        public GameObject enemyPrefab;

    }

    public List<Wave> waves;// List of all waves in the game
    public int currentWaveCount;// Index of current wave

    [Header("Spawner Attributes")]
    float spawnTimer;//Timer use to determine when to spawn next enemy
    public int enemiesAlive;
    public int maxEnemiesAllowed;// Max enemies allowed on map
    public bool maxEnemiesReached = false;
    public float waveInterval;//Time between each wave

    [SerializeField]
    List<Transform> relativeSpawnPoints;
    LevelTracker levelProgress;
    private Transform player;
    private bool isPaused=false;
    private int enemiesKilled=0;
    public int TOTAL_ENEMIES_KILLED
    {
        get { return enemiesKilled; }
        set
        {
            enemiesKilled += value ;
        }
    }

    public static EnemySpawnManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void OnEnable()
    {
        GameManager.OnStateChange += OnStateChange;
    }

    private void OnDisable()
    {
        GameManager.OnStateChange -= OnStateChange;
    }

    private void OnStateChange(GAME_STATE currentgameState)
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
           
            default:
                break;
        }

    }
        
    // Start is called before the first frame update
    void Start()
    {
        levelProgress = LevelTracker.Instance;
        //CalculateWaveQuota();
    }

    /*// Update is called once per frame
    void Update()
    {
        if (isPaused)
            return;
        //Checks if the wave had ended and next wave should start spawning
        if(currentWaveCount<waves.Count && waves[currentWaveCount].spawnedCount == waves[currentWaveCount].waveQuota|| enemiesAlive==0)
        {
            StartCoroutine(BeginNextWave());
        }
        else if(currentWaveCount==waves.Count-1&& enemiesAlive<=0)
        {
            Debug.Log("LevelFinished");
        }
        spawnTimer += Time.deltaTime;

      *//*  //Checks if its time to spawn next enemy
        if (spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }*//*
    }

    IEnumerator BeginNextWave()
    {
        yield return new WaitForSeconds(waveInterval);
        //Debug.Log("Wave " +(currentWaveCount+1)+" spawing");
        if(currentWaveCount<waves.Count-1)
        {
            currentWaveCount++;
            CalculateWaveQuota();
        }
    }

    //To Calculate total number of enemies
    void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;
        foreach (var enemyGroup in waves[currentWaveQuota].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }
        waves[currentWaveCount].waveQuota = currentWaveQuota;
    }*/
    /// <summary>
    /// It will stop spawning enemies if the amount of enemies on the map is maximum.
    /// This method will only spawn enemies in a particular wave until its time for next wave's enemies to be spawned.
    /// </summary>
    public void SpawnEnemies(EnemyData enemyToSpawn)
    {
        //Checks if the minimum number of enemies in the wave have been spawned
        /*     if(waves[currentWaveCount].spawnedCount < waves[currentWaveCount].waveQuota &&  !maxEnemiesReached)
             {
                 //Spawn each type of enemy until the quota is fille
                 foreach(var enemyGroup in waves[currentWaveCount].enemyGroups)
                 {
                     //Check if the minimum number of enemies of this type have been spawned
                     if(enemyGroup.spawnedCount < enemyGroup.enemyCount)
                     {
                         //Limit the number of enemies that can be spawned at once
                         if(enemiesAlive >= maxEnemiesAllowed)
                         {
                             maxEnemiesReached = true;
                             return;
                         }*/
                 if (isPaused) return;
                    player = PlayerMovement.Instance.transform;

                   //Spawns enemies  randomly in given spawn positions;
                    Vector2 spawnPosition =  relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position;
                     // GameObject enemy = Instantiate(enemyToSpawn.enemyPrefab, spawnPosition, Quaternion.identity);
                    GameObject enemy = ObjectPooler.Generate(enemyToSpawn.enemyPrefab.name, spawnPosition, Quaternion.identity);
                    // Debug.Log("Enemy spawned");
                //  GameObject enemy = MyPooler.ObjectPooler.Instance.GetFromPool(enemyToSpawn.enemyPrefab.name, spawnPosition, Quaternion.identity);
                   // enemy.SetActive(true);
                    enemy.name = enemyToSpawn.enemyPrefab.name;
                    Enemy newEnemy = enemy.GetComponent<Enemy>();
                    newEnemy.SetStats(enemyToSpawn.stats);
                    newEnemy.UpdateStatsForProgress(levelProgress.Progress);
        //Use Pooling Afterwards

        /*     enemyGroup.spawnedCount++;
             waves[currentWaveCount].spawnedCount++;
             enemiesAlive++;
         }
     }*/
    }

    /*    //Reset the flag if number of enemies alive has dropped below maximum amount
        if(enemiesAlive<maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
        }
    }*/

   /* public void OnEnemyKilled()
    {
        enemiesAlive--;
    }*/
}
