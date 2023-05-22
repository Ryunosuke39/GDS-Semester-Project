using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Unity.VisualScripting.Antlr3.Runtime;
using System.IO;
using UnityEditor.Animations;
using Unity.VisualScripting;

public class EnemyAI : MonoBehaviour
{
    public Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    public Transform enemyGFX;

    Pathfinding.Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    EnemyAIAttack enemyAIAttack;

    //enemy find player in range 
    public bool AwareOfPlayer { get; private set; }
    public Vector2 DirectionToPlayer { get; private set; }
    [SerializeField]
    private float playerAwarenessDistance;
    //public Transform targetPlayer;

    void Start()
    {
        //enemyAIPlayerInRange = FindRange.GetComponent<EnemyAIPlayerInRange>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        enemyAIAttack = GetComponent<EnemyAIAttack>();
        target = GameObject.Find("Player").transform;

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {

         if (seeker.IsDone())
              seeker.StartPath(rb.position, target.position, OnPathComplete);

         //transform.position -= transform.right * 0.2f;
        
    }

    //copied knockback function 
    private IEnumerator Knockback()
    {
        // Push enemy back a little bit
        transform.position -= transform.right * 0.2f;
        yield return new WaitForSeconds(0.1f);
    }

     void OnPathComplete(Pathfinding.Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void Update()
    {
        Vector2 enemyToPlayerVector = target.position - transform.position;
        DirectionToPlayer = enemyToPlayerVector.normalized;

        if(enemyToPlayerVector.magnitude <= playerAwarenessDistance)
        {
            AwareOfPlayer = true;
        }
        else
        {
            AwareOfPlayer = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (AwareOfPlayer)
        {
            if (path == null)
                return;

            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;

            //if (enemyAIAttack.isKnockbecked)
            //{
            //transform.position -= transform.right * 0.2f;
            //enemyAIAttack.isKnockbecked = false;
            //}
            //if(enemyAIAttack.isKnockbecked == false)
            //{ 
            rb.AddForce(force);
            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }
            //}


            //updtae GFX of enemy
            if (force.x >= 0.01f)
            {
                enemyGFX.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (force.x <= -0.01f)
            {
                enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
    }

}
