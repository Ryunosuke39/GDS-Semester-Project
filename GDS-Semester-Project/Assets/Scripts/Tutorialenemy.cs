using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorialenemy : MonoBehaviour
{
    public float speed = 0f;
    public float maxHp = 100f;
    public float currentHp;
    public float attackDamage = 34f;
    public float attackDelay = 1f;
    
    public GameObject door1;
    public GameObject door2;
    
    private Transform player;
    private bool isAlive = true;
    private bool isAttacking = false;
    private Vector2 direction;
    private Rigidbody2D rb;

    private bool playerInRange = false;

    private GameManager gameManager;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHp = maxHp;
        gameManager = GameObject.FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();

        door1.SetActive(false);
        door2.SetActive(false);
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        
        if (isAlive && player != null)
        {
            if (distanceToPlayer < 8f && !playerInRange)
            {
                playerInRange = true;

                door1.SetActive(true);
                door2.SetActive(true);
                
                direction = (player.position - transform.position).normalized;
            //transform.Translate(direction * speed * Time.deltaTime);

            //transform.position += new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;

                if (direction.x > 0)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else if (direction.x < 0)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
            }
            
        }



    }

    private void FixedUpdate()
    {
        if (isAlive && player != null)
        {
            rb.velocity = direction * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null && !isAttacking)
            {
                isAlive = false;
                StartCoroutine(AttackPlayer(player));
            }
            else
            {
                Debug.LogWarning($"Player component not found on collided object: {collision.gameObject.name}");

            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (isAlive)
        {
            currentHp -= damage;
            FindObjectOfType<AudioManager>().Play("EnemyInjured");//audio manager
            if (currentHp <= 0)
            {
                isAlive = false;
                FindObjectOfType<AudioManager>().Play("EnemyDeath");//audio manager
                Timer.Instance.AddTime(5);
                Destroy(gameObject);

                door1.SetActive(false);
                door2.SetActive(false);
            }
            else
            {
                // Knockback effect
                StartCoroutine(Knockback());
            }
        }
    }

    private IEnumerator Knockback()
    {
        // Push enemy back a little bit
        transform.position -= transform.right * 0.2f;

        // Change color to indicate damage taken
        GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(0.1f);

        // Change color back to normal
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private IEnumerator AttackPlayer(Player player)
    {
        isAttacking = true;
        FindObjectOfType<AudioManager>().Play("EnemyAttack");//audio manager
        yield return new WaitForSeconds(attackDelay);
        player.TakeDamage(attackDamage);
        Destroy(gameObject);
    }
}

