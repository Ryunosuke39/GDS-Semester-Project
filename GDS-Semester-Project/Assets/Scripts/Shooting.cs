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

        rotZ = ClampRotation(rotZ);

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

    private float ClampRotation(float rotZ)
    {
        bool isPressingA = Input.GetKey(KeyCode.A);
        bool isPressingD = Input.GetKey(KeyCode.D);
        bool isPressingW = Input.GetKey(KeyCode.W);
        bool isPressingS = Input.GetKey(KeyCode.S);
        

        float buffer = 5f;
        
        if (isPressingA && !isPressingD && !isPressingS && !isPressingW) 
        {
            if (rotZ >= 0 && rotZ <= 180 + buffer)
            {
                rotZ = Mathf.Clamp(rotZ, 90, 180);
            }
            else
            {
                rotZ = Mathf.Clamp(rotZ, -180, -90);
            }
        }
        else if (!isPressingA && isPressingD && !isPressingS && !isPressingW) 
        {
            rotZ = Mathf.Clamp(rotZ, -90, 90);
        }
        else if (isPressingW && !isPressingS && !isPressingA && !isPressingD) 
        {
            if(rotZ >= -180 + buffer && rotZ <= 180 - buffer)
            {
                rotZ = Mathf.Clamp(rotZ, 0, 180);
            }
        }  
        else if (!isPressingW && isPressingS && !isPressingA && !isPressingD) 
        {
            if(rotZ >= -180 + buffer && rotZ <= 180 - buffer)
            {
                rotZ = Mathf.Clamp(rotZ, -180, 0);
            }
        }
        else // Not pressing WASD
        {
            if (rotZ >= -90 && rotZ <= 90) //Right side
            {
                rotZ = Mathf.Clamp(rotZ, -90, 90);
            }
            else if (rotZ >= 90 && rotZ <= 270) // Left side
            {
                rotZ = Mathf.Clamp(rotZ, 90, 270);
            }
        }
        
        
        /*
        if(isPressingA)
        {
            if(rotZ >= 0 && rotZ <= 180)
            {
                rotZ = Mathf.Clamp(rotZ, 90, 180);
            }
            else
            {
                rotZ = Mathf.Clamp(rotZ, -180, -90);
            }
        }
        else if(isPressingD)
        {
            rotZ = Mathf.Clamp(rotZ, -90, 90);
        }
        if(isPressingW)
        {
            if(!isPressingA && !isPressingD)
            {
                rotZ = Mathf.Clamp(rotZ, 0, 180);
            }
            else
            {
                rotZ = Mathf.Clamp(rotZ, 0, rotZ);
            }
        }
        else if(isPressingS)
        {
            if(!isPressingA && !isPressingD)
            {
                rotZ = Mathf.Clamp(rotZ, -180, 0);
            }
            else
            {
                rotZ = Mathf.Clamp(rotZ, rotZ, 0);
            }
        }
        */

        /*
        if(isPressingA)
        {
            if(isPressingW)
            {
                rotZ = Mathf.Clamp(rotZ, 90, 180);
            }
            else if(isPressingS)
            {
                rotZ = Mathf.Clamp(rotZ, 0, 90);
            }
            else
            {
                rotZ = Mathf.Clamp(rotZ, 0, 180);
            }
        }
        else if(isPressingD)
        {
            if(isPressingW)
            {
                rotZ = Mathf.Clamp(rotZ, -180, -90);
            }
            else if(isPressingS)
            {
                rotZ = Mathf.Clamp(rotZ, -90, 0);
            }
            else
            {
                rotZ = Mathf.Clamp(rotZ, -180, 0);
            }
        }
        else if(isPressingW)
        {
            rotZ = Mathf.Clamp(rotZ, -90, 90);
        }
        else if(isPressingS)
        {
            rotZ = Mathf.Clamp(rotZ, 90, 270);
        }
        */
        return rotZ;
    }

    
}