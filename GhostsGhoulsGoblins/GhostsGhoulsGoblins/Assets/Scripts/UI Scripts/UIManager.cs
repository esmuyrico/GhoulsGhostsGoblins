using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Brough, Heath
// Created (4/22/2024)
// Last updated (4/3/2024)
// Handles UI navigation and holds functions to update the Coins, Health, and Dive Charge Meter

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private GameObject mainMenuScreen;
    [SerializeField]
    private GameObject currentMenuScreen;
    

    private GameObject canvasRef;

    // True when the player is in the MainMenus
    private bool notInGame;

    // HUD element references
    [SerializeField] private GameObject _HUDRef;
    [SerializeField] private GameObject HealthBarRef;
    [SerializeField] private GameObject DiveStrengthBar;
    [SerializeField] private GameObject MoneyTrackerRef;
    [SerializeField] private GameObject TipTextRef;
    [SerializeField] private GameObject ChoiceMenu;
    [SerializeField] private GameObject endScreenRef;
    // UI Element arrays
    [SerializeField] private GameObject[] _HUDElements = new GameObject[5];

    private bool isPaused = false;

    private void Awake()
    {
        currentMenuScreen = mainMenuScreen;
        TipTextRef = _HUDRef.transform.GetChild(0).gameObject;
        HealthBarRef = _HUDRef.transform.GetChild(1).gameObject;
        DiveStrengthBar = _HUDRef.transform.GetChild(2).gameObject;
        MoneyTrackerRef = _HUDRef.transform.GetChild(3).gameObject;
        canvasRef = mainMenuScreen.transform.parent.gameObject;

        // set all UI elements except the the main menu to inactive
        for (int child = 1; child < canvasRef.transform.childCount; child++)
        {
            canvasRef.transform.GetChild(child).gameObject.SetActive(false);
        }

        TipTextRef.GetComponent<TMP_Text>().text = "What an interesting looking sign, I should investigate";
    }
    void Start()
    {
        
       // Time.timeScale = 0;

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

    public void StartGame()
    {
        Time.timeScale = 1;
        mainMenuScreen.SetActive(false);
    }

    /// <summary>
    /// updates the health in the UI
    /// </summary>
    /// <param name="newHealthTotal">the new value that health is at</param>
    /// <param name="maxHealth">the maximum health the player can have</param>
    public void UpdateHealth(float newHealthTotal, float maxHealth)
    {
        float healthPercent = newHealthTotal / maxHealth;
        Debug.Log("Health: " + newHealthTotal);
        Debug.Log("Max Health: " + maxHealth);
        // update the health in the UI
        HealthBarRef.transform.GetChild(1).GetComponent<TMP_Text>().text = newHealthTotal.ToString();
        HealthBarRef.transform.GetChild(0).localScale = new Vector3(healthPercent , 1, 1);
        Debug.Log(healthPercent);
    }

    /// <summary>
    /// Updates the gold count in the UI
    /// </summary>
    /// <param name="newGoldTotal">The new amount of gold to display</param>
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

    public void TogglePause(GameObject PauseMenu)
    {
        // unpause
        if (isPaused)
        {
            Time.timeScale = 1;
            PauseMenu.SetActive(false);
        }
        // pause
        else
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
        }

            isPaused = !isPaused;
    }

    public void UpdateTipText(string tipText)
    {
        TipTextRef.SetActive(true);
        TipTextRef.GetComponent<TMP_Text>().text = tipText;
    }

    public void CloseTipText()
    {
        TipTextRef.GetComponent<TMP_Text>().text = "";
        TipTextRef.SetActive(false);
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void OpenChoiceMenu()
    {
        ChoiceMenu.SetActive(true);
    }

    public void CloseChoiceMenu()
    {
        ChoiceMenu.SetActive(false);
    }

    public void DisplayEndScreen()
    {
        string killerTitle = "Stone Cold Killer";
        string sparerTitle= "Heart of Gold";

        string title;

        if (GhostDialogue.Instance.ghoulKilled)
        {
            title = killerTitle;
        }
        else
        {
            title = sparerTitle;
        }

        endScreenRef.gameObject.SetActive(true);
        endScreenRef.transform.GetChild(0).GetComponent<TMP_Text>().text = "Title \n" + title;
        //endScreen.GetChild(1).GetComponent<TMP_Text>().text = "Score \n" + PlayerData.Instance.CoinNum
        Time.timeScale = 0;
    }
}
