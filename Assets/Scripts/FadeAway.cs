using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeAway : MonoBehaviour
{
    // the image you want to fade, assign in inspector
    public Image imgUp;
    public Button buttonPlay;

    void Start()
    {
        // fades the image out after the button click
        buttonPlay.onClick.AddListener(FadeImage);
    }

    public void FadeImage()
    {
        StartCoroutine(FadeImage(true));
    }
    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 4 second backwards
            for (float i = 2; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                imgUp.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
    }
}
