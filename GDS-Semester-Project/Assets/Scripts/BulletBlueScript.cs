using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBlueScript : MonoBehaviour
{

    //2D enemy shooting unity tutorial by MoreBBlakeyyy
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;

    //newly created 
    Transform gunDirection;//use thia rather than mouse so animation match with this 

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        //mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        //Vector3 direction = mousePos - transform.position;//direction for bullet
        AssignReference();
        Vector3 direction = gunDirection.position - transform.position;//newly created 
        //Vector3 rotation = transform.position - mousePos;
        Vector3 rotation = transform.position - gunDirection.position;//newly created 

        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot - 180);

    }

    void AssignReference()
    {
        gunDirection = GameObject.FindGameObjectWithTag("gunDirection").GetComponent<Transform>();
    }
 
}
