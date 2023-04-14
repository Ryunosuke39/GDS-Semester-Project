using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private GameObject trapPrefab;
    private Player player;
    private float damage = 50f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.playerTransform.GetComponent<Player>();
    }

    // Update is called once per frame
    private void onTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            player.TakeDamage(damage);
            Destroy(trapPrefab);
        }
    }
}
