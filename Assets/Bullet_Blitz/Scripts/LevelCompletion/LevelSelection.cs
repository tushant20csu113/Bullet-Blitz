using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField]
    List<Button> levels;
    [SerializeField]
    private int totalLevels;
    private int levelIndex = 0;
   
    private void OnEnable()
    {
        levelIndex = PlayerPrefs.GetInt("LevelsUnlocked");
       // Debug.Log(levelIndex);
        if (levelIndex > totalLevels - 1)
        {
            levelIndex = totalLevels - 1;
        }
        for (int i = 0; i <= levelIndex; i++)
        {
            levels[i].interactable = true;
        }
       /* for (int i = levelIndex + 1; i < levels.Count; i++)
        {
            levels[i].gameObject.transform.GetComponentInChildren<Text>().color = Color.red;
        }*/
    }
   

    
}
