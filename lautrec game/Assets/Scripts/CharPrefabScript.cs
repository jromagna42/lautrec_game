using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharPrefabScript : MonoBehaviour
{
    public bool dirRight = true;
    public GameObject spriteHolder;

    //CHAR DIRECTION
    public bool baseSpriteIsRight = true;
    public bool goingRight = true;
    float lastXPos = 0;

    //char up down movement
    float maxY = -10;
    float minY = -13;
    public float speed = 2;
    float realSpeed;
    public float maxTimer = 1.5f;
    public float minTimer = 0.5f;
    public float timer = 0;
    public bool goingUp = true;

    void Start()
    {
        //  foreach (Transform child in transform)
        // {
        //     child.rotation = Quaternion.Euler(0, 90, 0);
        // }
        goingUp = (Random.value > 0.5f)? true : false;
    }

    void MoveUpDown()
    {
        if (timer <= 0)
        {
            timer = Random.Range(minTimer, maxTimer);
            goingUp = !goingUp;
        }
        if (spriteHolder.transform.position.y < minY)
        {
            goingUp = true;
        }
        if (spriteHolder.transform.position.y > maxY)
        {
            goingUp = false;
        }
        realSpeed = (goingUp == true) ? speed : -speed;
        timer -= Time.deltaTime;
        foreach (Transform child in transform)
        {
            child.position = new Vector3(child.position.x, child.position.y + realSpeed * Time.deltaTime, child.position.z);
        }
        
    }

    void DirectionSprite()
    {
        float rotaY = 0;
        if (baseSpriteIsRight == false)
            rotaY = 180;
        if (spriteHolder.transform.position.x > lastXPos)
            goingRight = true;
        else
        {
            goingRight = false;
            rotaY += 180;
        }
        
        spriteHolder.transform.rotation = Quaternion.Euler(0, rotaY, 0);
        lastXPos = spriteHolder.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        MoveUpDown();
        DirectionSprite();
    }
}
