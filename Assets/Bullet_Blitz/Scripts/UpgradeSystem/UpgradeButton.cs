using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler
{
    Image icon;
    UpgradeData upgradeData;
    [SerializeField]
    UpgradePanelController upgradePM;
    [SerializeField]
    int buttonIndex;
    public UpgradeData DATA
    {
        get
        {
            return upgradeData;
        }
    }
    private void Awake()
    {
        icon = GetComponent<Image>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        upgradePM.Upgrade(buttonIndex);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (upgradeData != null)
            upgradePM.SetUpgradeInfo(upgradeData.upgradeInfo);
        else
            Debug.Log("Upgrade data is null");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (upgradeData != null)
            upgradePM.SetUpgradeInfo(" ");
    }
    public void Clean()
    {
        icon.sprite = null;
        this.upgradeData = null;
    }

    public void Set(UpgradeData upgradeData)
    {      
            icon.sprite = upgradeData.icon;
            this.upgradeData = upgradeData;
        
     }

}
