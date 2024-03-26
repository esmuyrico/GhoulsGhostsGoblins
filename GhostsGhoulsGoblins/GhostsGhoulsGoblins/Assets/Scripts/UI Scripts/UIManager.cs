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
    // Start is called before the first frame update
    void Start()
    {
        currentMenuScreen = mainMenuScreen;
        Time.timeScale = 0;
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
}
