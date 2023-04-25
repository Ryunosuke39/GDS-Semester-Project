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
        player.OnHealthChanged += UpdateHealthBar;
    }

    void UpdateHealthBar(float health)
{
    float healthPercent = health / player.maxHealth;
    healthBar.fillAmount = healthPercent;
    healthBarCanvas.transform.LookAt(mainCamera.transform);
}

    void OnDestroy()
{
    player.OnHealthChanged -= UpdateHealthBar;
}
}
