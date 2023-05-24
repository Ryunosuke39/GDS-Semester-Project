using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public GameObject keyPromptPanel;
    //public bool keyAcquired = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
         {
                keyPromptPanel.SetActive(true);
                 // Pause the game
            Time.timeScale = 0;
         }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            keyPromptPanel.SetActive(false);
            // Resume the game
        Time.timeScale = 1;
        }
    }
}
