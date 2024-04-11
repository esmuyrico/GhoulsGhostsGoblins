using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private int coinNum;
    // Start is called before the first frame update
    void Start()
    {
        coinNum = 0;
    }


    public void HurtPlayer()
    {

    }







    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            other.gameObject.SetActive(false);
            coinNum++;
            UIManager.Instance.UpdateGold(coinNum);
        }


        if (other.gameObject.tag == "Enemy")
        {

            HurtPlayer();







        }





    }

}
