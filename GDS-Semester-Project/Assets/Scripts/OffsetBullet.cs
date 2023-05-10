using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetBullet : MonoBehaviour
{
    public float offsetSpeed = 5f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Calculate the offset
        Vector3 offset = Quaternion.Euler(0, 0, 90f) * rb.velocity.normalized * offsetSpeed * Time.deltaTime;
        // Apply the offset to the bullet
        transform.position += offset;
    }
}
