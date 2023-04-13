using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2 moveDirection;
    public float moveSpeed = 5f;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float bulletDamage = 50f;
    public float fireRate = 0.5f;
    public float Health = 100.0f;

    //these two isplayerdead and is playerdeathplayed is for audio system for player
    private bool isPlayerDead = false;
    private bool isPlayerDeathPlayed = false;

    private Rigidbody2D rb;
    private Animator animator;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        ProcessInputs();//read input from player 

        if(Health <= 0)
        {
            GameManager.Instance.PlayerLost();
        }
    }

    private void FixedUpdate()
    {
        Move();//player movement 
    }

    void ProcessInputs()
    {
        if (!isPlayerDead)
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            moveDirection = new Vector2(moveX, moveY).normalized;
        }
    }

    void Move()
    {
        //if(!isPlayerDead)
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
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
        if(!isPlayerDead)
        FindObjectOfType<AudioManager>().Play("PlayerInjured"); //audio manager //a bit slow???

        if (Health <= 0)
        {
            Health = 0;
            isPlayerDead = true;
            if (isPlayerDead && !isPlayerDeathPlayed)
            {
                FindObjectOfType<AudioManager>().Play("PlayerDeath"); //Audio Manager
                isPlayerDeathPlayed = true;
            }
            //Destroy(gameObject); produce error from camera follow scritp 
            //gameObject.SetActive(false);
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
    public float boostedFireRate = 0.025f;//fire fast
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