using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI totalTimeText;
    [SerializeField] TextMeshProUGUI totalScoreText;
    [SerializeField] TextMeshProUGUI UpdateText;


    private void Start()
    {
        OnGameOver();
    }
    private void SetScore()
    {
        totalScoreText.text = ((int)LevelTracker.Instance.TotalTime *EnemySpawnManager.Instance.TOTAL_ENEMIES_KILLED*10).ToString();
    }

    void SetTime()
    {
        float currentTime = LevelTracker.Instance.TotalTime;
        int minutes = (int)(currentTime / 60f);
        int seconds = (int)(currentTime % 60f);

        totalTimeText.text = minutes.ToString() + ":" + seconds.ToString("00");
    }
  
    private void OnGameOver()
    {
        SetTime();
        SetScore();
        UpdateText.text = "DEAD";
    }
   
}
