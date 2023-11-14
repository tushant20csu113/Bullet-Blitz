using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExperienceBar : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] Slider slider;
   
    public  void UpdateExperienceSlider(int current,int target)
    {
        slider.maxValue = target;
        slider.value=Mathf.Lerp(slider.value, current, 0.3f);
        //Sslider.value = current;
    }
    public void SetLevelText(int level)
    {
        levelText.text = "LEVEL:" + level.ToString();
    }
}
