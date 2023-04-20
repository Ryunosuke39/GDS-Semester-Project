using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Player player;
    public Image healthBar;
    public Canvas healthBarCanvas;
    private Camera mainCamera;

      void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {

         float healthPercent = player.Health / player.maxHealth;
        healthBar.fillAmount = healthPercent;
        healthBarCanvas.transform.LookAt(mainCamera.transform);
    }
}
