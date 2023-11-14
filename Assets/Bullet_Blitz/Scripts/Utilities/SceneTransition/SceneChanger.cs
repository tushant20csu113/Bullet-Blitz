using UnityEngine;
using UnityEngine.EventSystems;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    private SceneTransitionMode transitionMode;
    [SerializeField]
    private string sceneName;

    public void LevelLoader()
    {
        SceneTransitioner.Instance.LoadScene(sceneName, transitionMode);
    }
    
}
