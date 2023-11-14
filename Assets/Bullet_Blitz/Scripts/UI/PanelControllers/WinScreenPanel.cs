using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class WinScreenPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI totalTimeText;
    [SerializeField] TextMeshProUGUI totalScoreText;
    [SerializeField] TextMeshProUGUI UpdateText;

    private void Start()
    {
        OnWin();
    }
    private void SetScore()
    {
        totalScoreText.text = ((int)LevelTracker.Instance.TotalTime * EnemySpawnManager.Instance.TOTAL_ENEMIES_KILLED * 10).ToString();
    }

    void SetTime()
    {
        float currentTime = LevelTracker.Instance.TotalTime;
        int minutes = (int)(currentTime / 60f);
        int seconds = (int)(currentTime % 60f);

        totalTimeText.text = minutes.ToString() + ":" + seconds.ToString("00");
    }
    public void OnWin()
    {
        SetTime();
        SetScore();
        UpdateText.text = "STAGE COMPLETED!";
        //UpdateText.color = Color.green;
    }
}
