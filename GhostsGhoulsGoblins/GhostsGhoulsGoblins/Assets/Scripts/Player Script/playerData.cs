using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Last Edited 4/22/2024 by Heath Brough
// Holds the player stats and a function to damage the player

public class PlayerData : Singleton<PlayerData>
{
    private int coinNum = 0;
    public int maxHealth = 10;
    public int health = 10;
    public int fallDamage = 2;

    // Start is called before the first frame update
    void Start()
    {
        coinNum = 0;
        UIManager.Instance.UpdateHealth(health, maxHealth);
        UIManager.Instance.UpdateGold(0);
    }

    public void HurtPlayer(int damage)
    {
        health-=damage;
        
        if (health <= 0)
        {
            Checkpoints.Instance.GoToCheckPoint();
            health = maxHealth;
        }
        UIManager.Instance.UpdateHealth(health, maxHealth);
    }

    private void Heal(int life)
    {
        health += life;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            other.gameObject.SetActive(false);
            coinNum++;
            UIManager.Instance.UpdateGold(coinNum);
        }

        if (other.tag == "HealthPack")
        {
            Heal(6);
            Destroy(other.gameObject);
            if (health > 10)
            {
                health = 10;
            }
        }

        if (other.gameObject.tag == "Enemy" && !gameObject.GetComponent<PlayerController>().isDiving)
        {
            //HurtPlayer(2);
        }

        if (other.CompareTag("Pit"))
        {
            Checkpoints.Instance.FellOff();
            HurtPlayer(fallDamage);
        }
    }
}
