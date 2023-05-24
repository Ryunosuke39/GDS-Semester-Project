using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class EnemyAIAttack : MonoBehaviour
{
    //public float speed = 3f;
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

    //assign BrainGFXX for changing the color 
    public Transform brainGFX;

    public bool isKnockbecked = false;

    //Animator
    Animator animator;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; ;//.transform;
        currentHp = maxHp;
        gameManager = GameObject.FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        animator = brainGFX.GetComponent<Animator>();
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
                animator.SetBool("isDead", true);
                Timer.Instance.AddTime(5);
                Destroy(gameObject, 0.5f);
                
            }
            else
            {
                // Knockback effect
                isKnockbecked = true;
                StartCoroutine(Knockback());
            }
        }
    }

    private IEnumerator Knockback()
    {
        // Push enemy back a little bit
        // transform.position -= transform.right * 0.2f;

        // Change color to indicate damage taken

        brainGFX.GetComponent<SpriteRenderer>().color = Color.red;
        FindObjectOfType<AudioManager>().Play("EnemyAttack");//audio manager
        animator.SetBool("isAttacked", true);
        //FindObjectOfType<AudioManager>().Play("EnemyAttack");//audio manager
        yield return new WaitForSeconds(attackDelay);

        // Change color back to normal
        brainGFX.GetComponent<SpriteRenderer>().color = Color.white;
        
    }

    private IEnumerator AttackPlayer(Player player)
    {
        isAttacking = true;
        FindObjectOfType<AudioManager>().Play("EnemyAttack");//audio manager
        animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(attackDelay);
        player.TakeDamage(attackDamage);
        Destroy(gameObject);
    }


}
