using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayScreenPanelController : MonoBehaviour
{
    [SerializeField] ExperienceBar expBar;//Experience Bar
    [SerializeField] TextMeshProUGUI currentHealthText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI objectiveText;
    public static PlayScreenPanelController Instance { get; private set; }
   


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
 
    }
    private void Start()
    {
      
        ObjText("Survive for 1 min to win!");
    
        expBar.UpdateExperienceSlider(LevelUpSystem.Instance.EXPERIENCE, LevelUpSystem.Instance.TO_LEVEL_UP);
        expBar.SetLevelText(LevelUpSystem.Instance.LEVEL);
    }
 
    public void ObjText(string objText)
    {
        objectiveText.transform.gameObject.SetActive(true);
        objectiveText.text = objText;
        TextFade tf=objectiveText.transform.GetComponent<TextFade>();
        StartCoroutine(tf.FadeTextToFullAlpha(0.2f, objectiveText));
        tf.FadeOut();
       /* Color textColor=objectiveText.transform.GetComponent<TextMeshProUGUI>().color;
        textColor.a = 100f;
        objectiveText.transform.GetComponent<TextMeshProUGUI>().color = textColor;*/


    }
    private void OnEnable()
    {
    ;
        PickUp.OnPickupExp += OnObjectPicked;
        LevelUpSystem.OnLevelUp += OnLevelUp;
        PlayerStats.OnHPchange += UpdateHP;
    }

    private void OnDisable()
    {
        PickUp.OnPickupExp -= OnObjectPicked;
        LevelUpSystem.OnLevelUp -= OnLevelUp;
        PlayerStats.OnHPchange += UpdateHP;
    }
    private void OnObjectPicked(ObjectType objType, int amount)
    {
        if (objType == ObjectType.EXP)
        {
            expBar.UpdateExperienceSlider(LevelUpSystem.Instance.EXPERIENCE, LevelUpSystem.Instance.TO_LEVEL_UP);
        }
        if (objType == ObjectType.HEALTH)
        {
            //Update Health bars
        }
    }
    private void OnLevelUp()
    {
        expBar.SetLevelText(LevelUpSystem.Instance.LEVEL);
    }
    private void UpdateHP(int currentHP, int maxHP)
    {
        currentHealthText.text = currentHP.ToString() + "/ " + maxHP.ToString();

    }
    public void UpdateTimer(float currentTime)
    {
        int minutes = (int)(currentTime / 60f);
        int seconds = (int)(currentTime % 60f);

        timerText.text = minutes.ToString() + ":" + seconds.ToString("00");
    }
}
