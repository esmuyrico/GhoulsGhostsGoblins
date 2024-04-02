using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // UI Element arrays
    [SerializeField] private GameObject[] _HUDElements = new GameObject[5];
    // Start is called before the first frame update
    void Start()
    {
        currentMenuScreen = mainMenuScreen;
        Time.timeScale = 0;

        _HUDRef = GameObject.FindGameObjectWithTag("HUD");

        int index = 0;
        GameObject currentChild;
        // finds all of the children of the HUD and puts them in a list
        while ((currentChild = _HUDRef.transform.GetChild(index).gameObject) != null)
        {
            _HUDElements[index] = currentChild;
            currentChild = null;
            index++;
        }
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

    public void UpdateHealth(int newHealthTotal)
    {
        // update the health in the UI
        

    }

    public void UpdateGold(int newGoldTotal)
    {
        // update the gold in the UI
    }

    public void UpdateDiveCharge()
    {

    }

}
