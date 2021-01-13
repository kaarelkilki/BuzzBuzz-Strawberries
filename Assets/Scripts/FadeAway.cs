using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeAway : MonoBehaviour
{
    // the image you want to fade, assign in inspector
    public Image imgUp;

    void Start()
    {
        // fades the image out on start
        if(GameObject.Find("CanvasPlay").activeInHierarchy == true)
        {
            StartCoroutine(FadeImage(true));
        }
    }

    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 3 second backwards
            for (float i = 4; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                imgUp.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
    }
}
