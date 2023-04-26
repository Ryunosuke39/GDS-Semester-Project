using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboarf : MonoBehaviour
{
    public GameObject keyPromptPanel;
    //public bool keyAcquired = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
         {
                keyPromptPanel.SetActive(true);
         }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            keyPromptPanel.SetActive(false);
        }
    }
}
