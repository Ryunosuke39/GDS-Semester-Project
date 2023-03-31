using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    public float maxHp = 100f;
    public float currentHp;

    private Transform player;
    private bool isAlive = true;

    private GameManager gameManager;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHp = maxHp;
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (isAlive) {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        if (isAlive) {
            currentHp -= damage;
            if (currentHp <= 0)
            {
                isAlive = false;
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
}
