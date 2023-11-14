using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextFade : MonoBehaviour
{
    private Color textColor;
    [SerializeField]
    private TextMeshProUGUI textToFade;
   
 
    public void FadeOut()
    {
        textColor = textToFade.color;
        StartCoroutine(FadeTextToZeroAlpha(3f,textToFade));
      //  StartCoroutine(FadeTextToFullAlpha(0.5f,textToFade));
        
    }
    public IEnumerator FadeTextToFullAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
    IEnumerator TextFader()
    {
        float disappearSpeed = 0.01f;
        while(textColor.a>0)
        {
   
            textColor.a -= disappearSpeed*0.1f;
            textToFade.color = textColor;
            Debug.Log(textColor.a);
            yield return null;
        }
        textColor.a = 1f;
       // textToFade.color = textColor;
        transform.gameObject.SetActive(false);
    }
}
