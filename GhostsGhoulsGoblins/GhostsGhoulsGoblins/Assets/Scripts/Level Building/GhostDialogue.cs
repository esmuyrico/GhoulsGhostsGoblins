using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostDialogue : Singleton<GhostDialogue>
{
    public bool ghoulKilled = false;

    private string killedText = "Why did you kill my friend the ghoul?? if anyone is a monster, its you. Boo hoo hoo";
    private string sparedText = "You are so kind! Thank you for sparing my friend the ghoul, I don't think I could stand to be dead without him!";
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (ghoulKilled)
                UIManager.Instance.UpdateTipText(killedText);
            else
                UIManager.Instance.UpdateTipText(sparedText);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.CloseTipText();
        }
    }

    public void setKilledStatus(bool killed)
    {
        ghoulKilled = killed;
    }
}
