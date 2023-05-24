using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject keyPromptPanel;
    public bool keyAcquired = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!keyAcquired)
            {
                keyPromptPanel.SetActive(true);
                // Pause the game
            Time.timeScale = 0;
            }
            else
            {
                Destroy(gameObject);
            }
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
