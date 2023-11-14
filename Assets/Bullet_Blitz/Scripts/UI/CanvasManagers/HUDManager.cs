using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HUDManager : MonoBehaviour
{
    [SerializeField] GameObject playScreenPanel;
    [SerializeField] GameObject upgradePanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject winScreenPanel;

    private UpgradePanelController upgradePanelController;
    public static HUDManager Instance { get; private set; }
    PlayerInput playerInputMap;
 
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        playerInputMap = new PlayerInput();
    }

    private void OnEnable()
    {
        playerInputMap.Enable();
        LevelTracker.OnStateChange += LevelState;

    }

    private void OnDisable()
    {
        playerInputMap.Disable();
        LevelTracker.OnStateChange -= LevelState;
    }

    private void LevelState(LEVEL_STATE currentlevelState)
    {
        switch (currentlevelState)
        {
            case LEVEL_STATE.COMPLETED:
                LevelComplete();
                break;
            case LEVEL_STATE.GAME_OVER:
                GameOver();
                break;
        }
    }

  
    private void Start()
    {
        playerInputMap.UI.Escape.performed += EscapeButtonPressed;
        upgradePanelController = upgradePanel.transform.GetComponent<UpgradePanelController>();
    }

    private void EscapeButtonPressed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!pausePanel.activeInHierarchy)
        {
            GameManager.OnStateChange?.Invoke(GAME_STATE.PAUSED);
            pausePanel.SetActive(true);
        }
        else if (!settingsPanel.activeInHierarchy)
        {
            GameManager.OnStateChange?.Invoke(GAME_STATE.RUNNING);
            pausePanel.SetActive(false);
        }
        else
            settingsPanel.SetActive(false);
    }
    
    public void OpenSettingsPanel()
    {
        settingsPanel.SetActive(true);
    }
    public void OpenUpgradePanel(List<UpgradeData> upgradeDatas)
    {
        upgradePanel.SetActive(true);      
        upgradePanelController.OpenPanel(upgradeDatas);
    }
    public void CloseUpgradePanel()
    {
        upgradePanel.SetActive(false);
    }
    private void LevelComplete()
    {
        winScreenPanel.SetActive(true);
    }
    private void GameOver()
    {
        gameOverPanel.SetActive(true);
     
    }


}
