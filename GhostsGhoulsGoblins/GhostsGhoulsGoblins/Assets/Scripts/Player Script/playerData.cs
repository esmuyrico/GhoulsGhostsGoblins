using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    private int coinNum;
    public Text coinNumText;

    // Start is called before the first frame update
    void Start()
    {
        coinNum = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            other.gameObject.SetActive(false);
            coinNum++;
            coinNumText.text = "Coin: " + coinNum;
        }
    }
}
