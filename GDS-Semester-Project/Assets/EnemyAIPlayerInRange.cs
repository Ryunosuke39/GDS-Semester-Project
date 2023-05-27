using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIPlayerInRange : MonoBehaviour
{
    public bool doesFind = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            doesFind = true;
        }
    }
}
