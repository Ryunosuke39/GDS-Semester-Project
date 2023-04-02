using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float bulletDamage = 50f;
    public float fireRate = 0.5f;
    public float Health = 100.0f;
    

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isFiring = false;
    private float timeSinceLastFire = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Handle movement input
        if (Input.GetKey(KeyCode.W)) {
            movement.y = 1;
        } else if (Input.GetKey(KeyCode.S)) {
            movement.y = -1;
        } else {
            movement.y = 0;
        }
        if (Input.GetKey(KeyCode.A)) {
            movement.x = -1;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        } else if (Input.GetKey(KeyCode.D)) {
            movement.x = 1;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        } else {
            movement.x = 0;
        }

        // Handle firing input
        if (Input.GetKey(KeyCode.Space)) {
            isFiring = true;
        } else {
            isFiring = false;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = movement.normalized * moveSpeed;

        // Fire bullets if spacebar is held down
        if (isFiring && timeSinceLastFire >= fireRate) 
        {
            FireBullet();
            timeSinceLastFire = 0f;
        }
        timeSinceLastFire += Time.deltaTime;
    }

    private void FireBullet()
    {
        // Create bullet object and set its initial position and rotation
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // Set bullet velocity based on player direction
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        Vector2 direction;
        if (movement.x != 0) {
        direction = new Vector2(movement.x, 0);
       } else {
        direction = new Vector2(Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.Deg2Rad), Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
       }
        bulletRb.velocity = direction.normalized * bulletSpeed;

        // Set bullet damage
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.damage = bulletDamage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy bullet if it collides with another object
        if (collision.gameObject.CompareTag("Bullet")) {
            Destroy(collision.gameObject);
        }
    }
    
    private void LateUpdate()
    {
        // Destroy bullets that leave the camera view
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets) {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(bullet.transform.position);
            if (screenPos.x < 0 || screenPos.x > Screen.width || screenPos.y < 0 || screenPos.y > Screen.height) {
                Destroy(bullet);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if(Health <= 0)
        {
            Health = 0;
            Destroy(gameObject);
        }
    }




      //Various items function realization code from Jacky
      //Item2
      public float boostedSpeed = 8f;//run fast
      private IEnumerator BoostSpeedCoroutine(float duration)
    {
        float originalSpeed = moveSpeed;
        moveSpeed = boostedSpeed;
        yield return new WaitForSeconds(duration);
        moveSpeed = originalSpeed;
    }

     public void BoostSpeed(float duration)
    {
        StartCoroutine(BoostSpeedCoroutine(duration));
    }

    //Item1
    public float boostedFireRate = 0.1f;//fire fast
    public void BoostFireRate(float duration)
   {
    StartCoroutine(BoostFireRateCoroutine(duration));
   }
   private IEnumerator BoostFireRateCoroutine(float duration)
   {
    float originalFireRate = fireRate;
    fireRate = boostedFireRate;
    yield return new WaitForSeconds(duration);
    fireRate = originalFireRate;
   }
    

}