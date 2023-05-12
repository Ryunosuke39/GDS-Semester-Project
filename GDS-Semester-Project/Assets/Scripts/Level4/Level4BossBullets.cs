using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4BossBullets : MonoBehaviour
{

    public float damage = 10f;
    public float BulletTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, BulletTime);
    }

    // Update is called once per frame
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
