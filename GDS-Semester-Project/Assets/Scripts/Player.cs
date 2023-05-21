using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public float maxHealth = 100.0f;//Jacky
    //public bool isGamePaused = false;//Jacky for win lose panel



    //these two isplayerdead and is playerdeathplayed is for audio system for player
    private bool isPlayerDead = false;
    private bool isPlayerDeathPlayed = false;

    //movement animatiom 
    private Rigidbody2D rb;
    public Animator animator;

    //for gun changeing reference
    Transform[] children;
    //Sprite currentGun;
    //gun sprites
    public Sprite handgun;
    public Sprite shotGun;
    public Sprite sniperGun;
    public Sprite machineGun;
    //different bullet 
    public GameObject handgunBullet;
    public GameObject shotGunBullet;
    public GameObject sniperBullet;
    public GameObject machinegunBullet;
    //reference for scripts 
    BulletBlueScript bullBlueScript;
    Bullet bullte;

    //what gun does player have?
    public GameObject Shotgun;

    //for shotgun
    public bool isShotgun = false;

    //player movement 
    Vector2 movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();

        //references for gun changing 
        //currentGun = gameObject.transform.FindChild("RotatePoint/BulletTransform/GunSprite");
        //bullBlueScript = gameObject.transform.FindChild("RotatePoint").GetComponent<BulletBlueScript>();
        //find current gun sprite reference 
        children = transform.GetComponentsInChildren<Transform>();

        Health = maxHealth;//Jacky for HP UI

        
    }

    private void Update()
    {
        //ProcessInputs();//read input from player 
        if (!isPlayerDead)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            //moveDirection = new Vector2(moveX, moveY).normalized;
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }

        // if (isGamePaused) //Jacky for win lose panel
        // {
        // return;
        // }

    }

    private void FixedUpdate()
    {
        //Move();//player movement 
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        //   if (isGamePaused) //Jacky for win lose panel
        // {
        // return;
        // }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy bullet if it collides with another object
        if (collision.gameObject.CompareTag("Bullet")) {
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.CompareTag("Boss"))
        {
            Level2Boss boss = collision.gameObject.GetComponent<Level2Boss>();
            if(boss != null)
            {
                boss.TakeDamage(bulletDamage);
            }
        }
        //added for ditecting enemy tae damage 
        /*
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyAIAttack enemy = collision.gameObject.GetComponent<EnemyAIAttack>();
            if (enemy != null)
            {
                enemy.TakeDamage(bulletDamage);
            }
        }
        */
        //Thomas for tut

        if (collision.gameObject.CompareTag("Door"))
        {
            Destroy(collision.gameObject);
        }
        
    }

    //gun change 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //hand gun
        if(collision.tag == "Handgun")
        {
            Debug.Log("Handgun collide");
            isShotgun = false;
            foreach(var child in children)
            {
                if(child.name == "GunSprit")
                {
                    child.GetComponent<SpriteRenderer>().sprite = handgun; //change sprite
                }
                if(child.name == "RotatePoint")
                {
                    child.GetComponent<Shooting>().bullet = handgunBullet;//change bullet prefad to handgun one
                    child.GetComponent<Shooting>().timeBetweenFiring = 0.5f;
                    //for damage, check bullet prefab 
                }
            }
        }

        //gun change 
        //shot gun
        if (collision.tag == "Shotgun")
        {
            Debug.Log("shotGun collide");
            isShotgun = true;
            foreach (var child in children)
            {
                if (child.name == "GunSprit")
                {
                    child.GetComponent<SpriteRenderer>().sprite = shotGun; //change sprite
                }
                if(child.name == "RotatePoint")
                {
                    child.GetComponent<Shooting>().bullet = shotGunBullet;//change bullet prefab to shotgun one(certal bullet)
                    child.GetComponent<Shooting>().timeBetweenFiring = 0.5f;
                    //for damage, check prehab 
                }
            }
        }

        //sniper
        //gun change 
        if (collision.tag == "Snipergun")
        {
            
            Debug.Log("Snipergun collide" + isShotgun);
            isShotgun = false;
            foreach (var child in children)
            {
                if (child.name == "GunSprit")
                {
                    child.GetComponent<SpriteRenderer>().sprite = sniperGun; //change sprite
                }
                if(child.name == "RotatePoint")
                {
                    child.GetComponent<Shooting>().bullet = sniperBullet;
                    child.GetComponent<Shooting>().timeBetweenFiring = 2f;//5 second to reload
                    //for damage, check prehab 
                }
            }
        }

        //Machinegun
        //gun change 
        if (collision.tag == "Machinegun")
        {
            Debug.Log("Machinegun collide");
            isShotgun = false;
            foreach (var child in children)
            {
                if (child.name == "GunSprit")
                {
                    child.GetComponent<SpriteRenderer>().sprite = machineGun; //change sprite
                }
                if(child.name == "RotatePoint")
                {
                    child.GetComponent<Shooting>().bullet = machinegunBullet;
                    child.GetComponent<Shooting>().timeBetweenFiring = 0.1f;
                    //for damage, check prehab 
                }
            }
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
            animator.SetBool("Dead", true);
            Health = 0;
            isPlayerDead = true;
            if (isPlayerDead && !isPlayerDeathPlayed)
            {
                FindObjectOfType<AudioManager>().Play("PlayerDeath"); //Audio Manager
                //animator.SetBool("Dead", true);  //player player dead animation 
                isPlayerDeathPlayed =true;
            }
            //Destroy(gameObject); produce error from camera follow scritp 
             Die();//Jacky for lose panel
        }
         OnHealthChanged?.Invoke(Health);//Jacky 
    }
     
     //Jacky for lose panel when player die
    private void Die()
    {
        GameManager.Instance.PlayerLost();
    }

      //Various items function realization code from Jacky
      // Item2
private IEnumerator BoostSpeedCoroutine(float duration, float boostedSpeed)
{
    float originalSpeed = moveSpeed;
    moveSpeed = boostedSpeed;
    Debug.Log("BoostSpeedCoroutine: Speed increased to " + moveSpeed);
    yield return new WaitForSeconds(duration);
    moveSpeed = originalSpeed;
    Debug.Log("BoostSpeedCoroutine: Speed reset to " + moveSpeed);
}

public void BoostSpeed(float duration)
{
    float boostedSpeed = moveSpeed * 1.5f;
    Debug.Log("Player.BoostSpeed called");
    StartCoroutine(BoostSpeedCoroutine(duration, boostedSpeed));
}

// Item1
private IEnumerator BoostFireRateCoroutine(float duration, float boostedFireRate)
{
    float originalFireRate = fireRate;
    fireRate = boostedFireRate;
    Debug.Log("BoostFireRateCoroutine: Fire rate increased to " + fireRate);
    yield return new WaitForSeconds(duration);
    fireRate = originalFireRate;
    Debug.Log("BoostFireRateCoroutine: Fire rate reset to " + fireRate);
}

public void BoostFireRate(float duration)
{
    float boostedFireRate = fireRate * 15f;
    Debug.Log("Player.BoostFireRate called");
    StartCoroutine(BoostFireRateCoroutine(duration, boostedFireRate));
}

// Item0 add HP
public void AddHealth(float amount)
{
    Debug.Log("Player.AddHealth called");
    Health += amount;
    if (Health > maxHealth)
    {
        Health = maxHealth;
    }
    OnHealthChanged?.Invoke(Health);
}

//Jacky for update UI HP
public delegate void HealthChangedDelegate(float health);
public event HealthChangedDelegate OnHealthChanged;




}