using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDisactivation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("Bee").GetComponent<Bee>().addCount == 2)
        {
            GameObject.Find("RewardedAdsButton").SetActive(false);
        }
    }
}
