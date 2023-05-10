using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level4Boss : MonoBehaviour
{
    public float health = 1000f;
    private float maxHealth;
    public GameObject healthBarUI;
    public Slider healthSlider;
    public GameObject bossBulletPrefab;
    public float skillCooldown = 5f;
    public float bulletSpeed = 5f;

    public GameObject bossRapidBulletPrefab;
    public float homingBulletSpeed = 5f;
    public float rapidBulletDuration = 2f;
    public int rapidBulletsPerSecond = 10;

    public float rotatingCrossDuration = 2f;
    public int bulletPerDirection = 10;
    public float rotationSpeed = 45f;

    public GameObject door1;
    public GameObject door2;

    private Player player;
    private float skillTimer;
    private bool playerInRange = false;

    public float speed = 2.0f;

    

    void Start()
    {
        maxHealth = health;
        player = FindObjectOfType<Player>();
        skillTimer = skillCooldown;

        door1.SetActive(false);
        door2.SetActive(false);
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < 16f && !playerInRange) 
        {
            playerInRange = true;

            door1.SetActive(true);
            door2.SetActive(true);
        }
        if(playerInRange)
        {
            
                 if(healthBarUI != null)
                 {healthBarUI.SetActive(true);}
                 
            skillTimer -= Time.deltaTime;
            if(skillTimer <= 0)
            {
                SelectSkill();
                skillTimer = skillCooldown;
            }

            Vector3 playerPosition = player.transform.position;
            Vector3 direction = (playerPosition - transform.position).normalized;  //计算出向玩家移动的方向
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);  //使Boss向玩家移动
        }
        else
        {
            if(healthBarUI != null)
            {healthBarUI.SetActive(false);}
        }

        healthSlider.value = health / maxHealth;
    }

    private void SpawnBullets()
    {
        int numberOfFans = 2;
        int numberOfBulletsPerFan = 10;
        float angleBetweenBullets = 20f;

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

    private void StartSpawingRapidBullets()
    {
        InvokeRepeating("SpawnRapidBullet", 0f, 1f / rapidBulletsPerSecond);
        Invoke("StopSpawningRapidBullets", rapidBulletDuration);
    }

    private void SpawnRapidBullet()
    {
        GameObject rapidBullet = Instantiate(bossRapidBulletPrefab, transform.position, Quaternion.identity);
        rapidBullet.GetComponent<Rigidbody2D>().velocity = (player.transform.position - transform.position).normalized * homingBulletSpeed;
    }

    private void StopSpawingRapidBullets()
    {
        CancelInvoke("SpawnRapidBullet");
    }

    private void SpawnRotatingCrossBullets()
    {
        StartCoroutine(RotatingCrossBulletCoroutine());
    }

    private IEnumerator RotatingCrossBulletCoroutine()
    {
        float elapsedTime = 0f;
        int currentBulletOffset = 0;
        float timeBetweenBullets = rotatingCrossDuration / bulletPerDirection;
        float nextBulletTime = 0f;

        while(elapsedTime < rotatingCrossDuration)
        {
            if(elapsedTime >= nextBulletTime)
            {
                for(int i = 0; i < bulletPerDirection; i++)
                {
                    float angle = (i + currentBulletOffset) * 360f / bulletPerDirection;
                    Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.right;

                    GameObject bullet = Instantiate(bossBulletPrefab, transform.position, Quaternion.identity);
                    bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

                    bullet.AddComponent<OffsetBullet>();
                }

                nextBulletTime += timeBetweenBullets;

                currentBulletOffset = (currentBulletOffset + 1) % bulletPerDirection;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    

    private void SelectSkill()
    {
        int skillIndex = Random.Range(0, 3);
        switch(skillIndex)
        {
            case 0:
            SpawnBullets();
            break;
            case 1:
            StartSpawingRapidBullets();
            break;
            case 2:
            SpawnRotatingCrossBullets();
            break;
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
            //Destroy(healthBarUI);
            healthBarUI.SetActive(false);
        }

        door1.SetActive(false);
        door2.SetActive(false);
    }
}
