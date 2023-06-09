using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    public float maxHp = 100f;
    public float currentHp;
    public float attackDamage = 34f;
    public float attackDelay = 1f;

    private Transform player;
    private bool isAlive = true;
    private bool isAttacking = false;
    private Vector2 direction;
    private Rigidbody2D rb;

    private GameManager gameManager;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHp = maxHp;
        gameManager = GameObject.FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isAlive && player != null) {
            direction = (player.position - transform.position).normalized;
            //transform.Translate(direction * speed * Time.deltaTime);

            //transform.position += new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;

            if(direction.x > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if(direction.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }

        
        
    }

    private void FixedUpdate()
    {
        if(isAlive && player != null)
        {
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isAttacking)
        {
            isAlive = false;
            StartCoroutine(AttackPlayer(collision.GetComponent<Player>()));
        }
    }

    public void TakeDamage(float damage)
    {
        if (isAlive) {
            currentHp -= damage;
            FindObjectOfType<AudioManager>().Play("EnemyInjured");//audio manager
            if (currentHp <= 0)
            {
                isAlive = false;
                FindObjectOfType<AudioManager>().Play("EnemyDeath");//audio manager
                Destroy(gameObject);
            } else {
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
