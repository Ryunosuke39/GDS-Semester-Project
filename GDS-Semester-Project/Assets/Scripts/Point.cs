using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{

    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameManager.Instance.playerTransform;
        playerTransform.position = startPoint.position;
    }

    // Update is called once per frame
    void Update()
    {

        if(playerTransform == null)
        {
            return;
        }
        
        if(Vector3.Distance(playerTransform.position, endPoint.position) <= 0.5f)
        {
            GameManager.Instance.Win();
        }
    }
}
