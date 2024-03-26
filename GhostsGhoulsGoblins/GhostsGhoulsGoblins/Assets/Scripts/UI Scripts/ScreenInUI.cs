using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Brough, Heath
// 3/14/24
// template script for menu screens

public class ScreenInUI : MonoBehaviour
{
    // holds the screen to return to when pressing esc/going 
    private GameObject previousScreen;
    // holds references to the next possible screens that the player could select
    private GameObject[] NextScreens;

    // enables the previous screen 
    public void GoPreviousScreen()
    {
        previousScreen.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void GoNextScreen(GameObject nextScreen)
    {
        nextScreen.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
