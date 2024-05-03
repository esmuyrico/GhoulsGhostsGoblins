using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;


public class GhoulDialogue : Singleton<GhoulDialogue>
{

    private string beggingText = "I'm sorry for stealing all your gold and trapping you in this dungeon. Will you forgive me?";
    private string sparedText = "Thank you for sparing me! I promise I'll turn my death around";


    public bool choiceMade = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !choiceMade)
        {
            UIManager.Instance.UpdateTipText(beggingText);
            UIManager.Instance.OpenChoiceMenu();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UIManager.Instance.CloseChoiceMenu();
            UIManager.Instance.CloseTipText();
        }
    }

    public void Choice(bool killed)
    {
        choiceMade = true;
        GhostDialogue.Instance.setKilledStatus(killed);
        if (killed)
        {
            StartCoroutine(Killed());
            UIManager.Instance.CloseTipText();
        }
        else
        {
            Spared();
        }
        UIManager.Instance.CloseChoiceMenu();
        
    }

    public IEnumerator Killed()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.6f);
        transform.position -= new Vector3(0, 0.5f, 0);
        transform.rotation = Quaternion.Euler(0, 0, -90);
        StartCoroutine(BloodSpilled());
        // play gunshot sound
        // put ghoul on the ground
        // make pool of blood
    }

    public void Spared()
    {
        UIManager.Instance.UpdateTipText(sparedText);
    }

    private IEnumerator BloodSpilled()
    {
        Transform blood = transform.Find("blood"); 
        for (float i = 0; i < 1.8f; i+=0.05f)
        {
            blood.localScale = new Vector3(i, blood.localScale.y, i);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
