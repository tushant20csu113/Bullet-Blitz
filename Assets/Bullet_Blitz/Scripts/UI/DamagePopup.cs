using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamagePopup : MonoBehaviour
{
    [SerializeField] float timeToLive = 1f;
    float ttl = 2f;
    private TextMeshPro textMesh;
    private Color textColor;
    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
        textColor = textMesh.color;
    }
    private void OnEnable()
    {
        ttl = timeToLive;
    }
 

    // Update is called once per frame
    void Update()
    {
        float moveYSpeed = 1.5f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

        ttl -= Time.deltaTime;
        if(ttl<0f)
        {
            float disappearSpeed = 3f;

            textColor.a-=disappearSpeed * Time.deltaTime;
            textMesh.color=textColor;
            if(textColor.a < 0)
            gameObject.SetActive(false);
        }
    }
}
