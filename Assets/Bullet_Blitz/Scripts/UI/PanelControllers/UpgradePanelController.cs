using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradePanelController : MonoBehaviour
{
    
    [SerializeField] List<UpgradeButton> upgradeButtons;
    [SerializeField] TextMeshProUGUI upgradeInfoText;
     //Actions after panel is active
    public void OpenPanel(List<UpgradeData> upgradeDatas)
    {
        Clean();
        //Assign respective buttons with upgrade data
        for(int i=0;i<upgradeDatas.Count;i++)
        {     
            upgradeButtons[i].Set(upgradeDatas[i]);
        }
        //Disable buttons with no upgrade data
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            if(upgradeButtons[i].DATA == null)
            upgradeButtons[i].gameObject.SetActive(false);
        }
    }
    //Setting button data and sprite to null/clean state for new data
    public void Clean()
    {
        for(int i=0;i<upgradeButtons.Count;i++)
        {
            upgradeButtons[i].gameObject.SetActive(true);
            upgradeButtons[i].Clean();
        }
    }
    //Send which upgrade button is pressed
    public void Upgrade(int pressedButtonID)
    {
        LevelUpSystem.Instance.Upgrade(pressedButtonID);
        HideButtons();
        HUDManager.Instance.CloseUpgradePanel();
    }
    // Sets info for Upgrade Info
    public void SetUpgradeInfo(string upgradeInfo)
    {
        upgradeInfoText.text = upgradeInfo.ToString();   
    }
    //Hides all the buttons
    void HideButtons()
    {
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
        
            upgradeButtons[i].gameObject.SetActive(false);
            
        }
    }
}
