using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire;
    public float Firetimer;
    public float timeBetweenFiring;

    //reference to player 
    Player player;
    //for shotgun
    public GameObject shotgunBullet1;
    public GameObject shotgunBullet2;

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        
        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);//test 
        //transform.rotation = Quaternion.Euler(0, 0, rotZ - (rotZ / 2));

        //flip gunsprite when player point gun at left side
        Vector3 alocalScale = Vector3.one;
        if(rotZ > 90 || rotZ < -90)
        {
            alocalScale.y = -1f;
        }
        else
        {
            alocalScale.y = +1f;
        }
        transform.localScale = alocalScale;

        //Fire rate 
        if(!canFire)
        {
            Firetimer += Time.deltaTime;
            if(Firetimer > timeBetweenFiring)
            {
                canFire = true;
                Firetimer = 0;
            }
        }
        if(Input.GetMouseButton(0) && canFire)
        {
            canFire = false;
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
            
            if(player.isShotgun)
            {
                Instantiate(shotgunBullet1, bulletTransform.position, Quaternion.identity);
                Instantiate(shotgunBullet2, bulletTransform.position, Quaternion.identity);
            }
            
        }
    }

    
}