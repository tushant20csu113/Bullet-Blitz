using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum GAME_STATE
{
    STARTED,
    PAUSED,
    RUNNING
}
public class GameManager : MonoBehaviour
{
    [SerializeField] Texture2D crossHair;
    public static GameManager Instance { get; private set; }
    public delegate void CurrentGameState(GAME_STATE currentgameState);
    public static CurrentGameState OnStateChange;
    private int levelIndex;

    private void Awake()
    {
       /* if (crossHair != null)
        {
            Vector2 hotspot = new Vector2(crossHair.width / 2, crossHair.height / 2);
            Debug.Log(hotspot);
            Cursor.SetCursor(crossHair, hotspot, CursorMode.Auto);
        }*/
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        levelIndex = PlayerPrefs.GetInt("LevelsUnlocked");
    }
    private void OnEnable()
    {
        LevelTracker.OnStateChange += LevelState;
    }

    private void OnDisable()
    {
        LevelTracker.OnStateChange -= LevelState;
    }
    private void LevelState(LEVEL_STATE currentlevelState)
    {
        switch (currentlevelState)
        {
            case LEVEL_STATE.COMPLETED:
                UnlockNextLevel();
                break;
            case LEVEL_STATE.GAME_OVER:
                break;
        }
    }

    private void UnlockNextLevel()
    {
        levelIndex += 1;
        PlayerPrefs.SetInt("LevelsUnlocked", levelIndex);
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("Loading", LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
