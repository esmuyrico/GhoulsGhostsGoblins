using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayTipInUI : MonoBehaviour
{
    [SerializeField] string textToDisplay;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }

    private void openText()
    {
        UIManager.Instance.UpdateTipText(textToDisplay);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            openText();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.CloseTipText();
        }
    }

}
