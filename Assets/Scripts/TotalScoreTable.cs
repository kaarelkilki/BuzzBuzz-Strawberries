using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private void Awake()
    {
        entryContainer = transform.Find("TotalScoreEntryContainer");
        entryTemplate = entryContainer.Find("TotalScoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        float templateHight = 30f;
        for (int i = 0; i < 10; i++)
        {
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHight * i);
            entryTransform.gameObject.SetActive(true);

            int rank = i + 1;
            entryTransform.Find("posText").GetComponent<Text>().text = rank.ToString();

            int score = Random.Range(0, 100000);
            entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();

            string name = "AAA";
            entryTransform.Find("nameText").GetComponent<Text>().text = name;

        }
    }
}
