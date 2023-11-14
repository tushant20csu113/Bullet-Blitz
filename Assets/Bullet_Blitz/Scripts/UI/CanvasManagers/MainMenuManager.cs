using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] GameObject backToMenu;
    [SerializeField] GameObject buttonsPanel;
    [SerializeField] GameObject settingPanel;
    [SerializeField] GameObject levelSelectionPanel;

    public void StartGame()
    {
        title.transform.gameObject.SetActive(false);
        backToMenu.SetActive(true);
        buttonsPanel.SetActive(false);
        settingPanel.SetActive(false);
        levelSelectionPanel.SetActive(true);
    }
    public void Settings()
    {
        title.transform.gameObject.SetActive(false);
        backToMenu.SetActive(true);
        buttonsPanel.SetActive(false);
        settingPanel.SetActive(true);
        levelSelectionPanel.SetActive(false);
    }
    public void BackToMenu()
    {
        title.transform.gameObject.SetActive(true);
        buttonsPanel.SetActive(true);
        backToMenu.SetActive(false);
        settingPanel.SetActive(false);
        levelSelectionPanel.SetActive(false);
    }
    public void Quit()
    {
#if (UNITY_EDITOR || DEVELOPMENT_BUILD)
        Debug.Log(this.name + " : " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif
#if (UNITY_EDITOR)
        UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE) 
    Application.Quit();
#elif (UNITY_WEBGL)
    Application.OpenURL("about:blank");
#endif
    }
}
