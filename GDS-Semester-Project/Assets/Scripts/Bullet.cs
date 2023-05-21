using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 50.0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyAIAttack enemy = other.GetComponent<EnemyAIAttack>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log("enemy take damage");
                Destroy(gameObject);
            }
        }
        if(other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
