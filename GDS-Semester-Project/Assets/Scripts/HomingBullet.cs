using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    public float homingSpeed = 5f; 
    public float damage = 10f;

    private Rigidbody2D rb;
    private Transform target;

    void Start()
    {
        Destroy(gameObject, 5f);
    }

    

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if(player != null)
            {
                player.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
