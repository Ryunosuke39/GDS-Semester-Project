using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level2Boss : MonoBehaviour
{
    public float health = 1000f;
    private float maxHealth;
    public GameObject healthBarUI;
    public Slider healthSlider;

    private Player player;

    void Start()
    {
        maxHealth = health;
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < 10f) 
        {
            healthBarUI.SetActive(true);
        }
        else
        {
            healthBarUI.SetActive(false);
        }

        healthSlider.value = health / maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthSlider.value = health / maxHealth;

        if (health <= 0)
        {
            Die();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if(bullet != null)
            {
                TakeDamage(bullet.damage);
                Destroy(collision.gameObject);
            }
            else
            {
                Debug.LogWarning("Bullet component not found on collided object");
            }
        }
    }

    private void Die()
    {
        
    }
}
