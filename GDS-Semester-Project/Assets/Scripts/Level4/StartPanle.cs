using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPanle : MonoBehaviour
{
    public GameObject StartPanel;
    //public bool keyAcquired = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
         {
                StartPanel.SetActive(true);
                 // Pause the game
            Time.timeScale = 0;
         }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartPanel.SetActive(false);
            // Resume the game
        Time.timeScale = 1;
        }
    }
}
