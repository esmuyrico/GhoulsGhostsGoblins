using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuScreen;
    [SerializeField]
    private GameObject currentMenuScreen;

    // True when the player is in the MainMenus
    private bool notInGame;

    // HUD element references
    [SerializeField] private GameObject _HUDRef;
    [SerializeField] private GameObject HealthBarRef;
    [SerializeField] private GameObject DiveStrengthBar;
    [SerializeField] private GameObject MoneyTrackerRef;

    // UI Element arrays
    [SerializeField] private GameObject[] _HUDElements = new GameObject[5];
    // Start is called before the first frame update
    void Start()
    {
        currentMenuScreen = mainMenuScreen;
        Time.timeScale = 0;

        _HUDRef = GameObject.FindGameObjectWithTag("HUD");

        /*int index = 0;
        GameObject currentChild;
        // finds all of the children of the HUD and puts them in a list
        while ((currentChild = _HUDRef.transform.GetChild(index).gameObject) != null)
        {
            _HUDElements[index] = currentChild;
            currentChild = null;
            index++;
        }*/

        HealthBarRef = _HUDRef.transform.GetChild(1).gameObject;
        DiveStrengthBar = _HUDRef.transform.GetChild(2).gameObject;
        MoneyTrackerRef = _HUDRef.transform.GetChild(3).gameObject;
    }

    

    /// <summary>
    /// Returns the screen to the Main Menu Screen when the player has not started playing
    /// </summary>
    /// <param name="currentPanel"></param>
    public void ToMainMenu()
    {
        if (notInGame)
        {
            mainMenuScreen.SetActive(true);
            currentMenuScreen.SetActive(false);
            currentMenuScreen = mainMenuScreen;
        }
    }

    /// <summary>
    /// sets the next screen active 
    /// </summary>
    /// <param name="MenuToGoTo">Next menu screen to activate</param>
    public void GoNextScreen(GameObject MenuToGoTo)
    {
        currentMenuScreen.SetActive(false);
        MenuToGoTo.SetActive(true);
        currentMenuScreen = MenuToGoTo;

        if (MenuToGoTo.name == "HUD")
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        Time.timeScale = 1;
    }

    /// <summary>
    /// updates the health in the UI
    /// </summary>
    /// <param name="newHealthTotal">the new value that health is at</param>
    /// <param name="maxHealth">the maximum health the player can have</param>
    public void UpdateHealth(int newHealthTotal, int maxHealth)
    {
        float healthPercent = newHealthTotal / maxHealth;
        // update the health in the UI
        HealthBarRef.transform.GetChild(1).GetComponent<TMP_Text>().text = newHealthTotal.ToString();
        HealthBarRef.transform.GetChild(0).localScale = new Vector3(healthPercent , 1, 1);
    }

    public void UpdateGold(int newGoldTotal)
    {
        // update the gold in the UI
        MoneyTrackerRef.transform.GetChild(0).GetComponent<TMP_Text>().text = newGoldTotal.ToString();
    }

    /// <summary>
    /// updates the charge percentage in the UI
    /// </summary>
    /// <param name="percentComplete">number from 0-1 indicating the completeness of the charge 0 uncharged, 1 fully charged</param>
    public void UpdateDiveCharge(float percentComplete)
    {
        if (percentComplete == 0)
        {
            DiveStrengthBar.GetComponent<Image>().color = Color.grey;
            DiveStrengthBar.GetComponent<RectTransform>().transform.localScale = new Vector3(0.1f, 1, 1);
        }
        else
        {
            DiveStrengthBar.GetComponent<Image>().color = Color.blue;
            DiveStrengthBar.GetComponent<RectTransform>().transform.localScale = new Vector3(0.1f + 0.9f * percentComplete, 1, 1);
        }

    }
}
