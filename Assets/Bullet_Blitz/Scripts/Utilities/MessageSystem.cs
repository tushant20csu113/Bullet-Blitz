using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSystem : MonoBehaviour
{
    [SerializeField] GameObject damageMessage;
    [SerializeField]
    private int objectCount;
    List<TMPro.TextMeshPro> messagePool;
    private int count;

    public static MessageSystem Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        messagePool = new List<TMPro.TextMeshPro>();
        for(int i=0;i<objectCount;i++)
        {
            Populate();
        }
    }
    private void Populate()
    {
        GameObject popup = Instantiate(damageMessage, transform);
        messagePool.Add(popup.GetComponent<TMPro.TextMeshPro>());
        popup.SetActive(false);
    }
    public void PostMessage(string text,Vector3 worldPosition)
    {
        messagePool[count].gameObject.SetActive(true);
       messagePool[count].transform.position = worldPosition;
        messagePool[count].text = text;
        count += 1;
        if(count>=objectCount)
        {
            count = 0;
        }
    }
}
