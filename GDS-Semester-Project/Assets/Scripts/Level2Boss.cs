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
    public GameObject bossBulletPrefab;
    public float skillCooldown = 5f;
    public float bulletSpeed = 5f;

    private Player player;
    private float skillTimer;

    void Start()
    {
        maxHealth = health;
        player = FindObjectOfType<Player>();
        skillTimer = skillCooldown;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < 10f) 
        {
            healthBarUI.SetActive(true);
            skillTimer -= Time.deltaTime;
            if(skillTimer <= 0)
            {
                SpawnBullets();
                skillTimer = skillCooldown;
            }
        }
        else
        {
            healthBarUI.SetActive(false);
        }

        healthSlider.value = health / maxHealth;
    }

    private void SpawnBullets()
    {
        int numberOfFans = 5;
        int numberOfBulletsPerFan = 10;
        float angleBetweenBullets = 10f;

        float randomAngleOffset = Random.Range(-45f, 45f);

        for(int fan = 0; fan < numberOfFans; fan++)
        {
            float initialAngle = fan * 360f / numberOfFans;

            for(int i = 0; i < numberOfBulletsPerFan; i++)
            {
                float angle = initialAngle + i * angleBetweenBullets + randomAngleOffset;
                Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.right;

                GameObject bullet = Instantiate(bossBulletPrefab, transform.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
            }
        }
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
                
            }
        }
    }

    private void Die()
    {
        Destroy(gameObject);

        if(healthBarUI != null)
        {
            Destroy(healthBarUI);
        }
    }
}
