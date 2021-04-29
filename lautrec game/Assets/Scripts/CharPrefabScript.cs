using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharPrefabScript : MonoBehaviour
{
    public enum bigLayer {back,middle,front};

    Dictionary<string, int> bigLayerValue = new Dictionary<string, int>()
    {
        {"back", 1},
        {"middle", 5},
        {"front", 10}
    };
    
    public GameObject spriteHolder;
    public GameObject poteau;
    public bigLayer layer;
    public bool dirRight = true;
    public bool isMoving = true;

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
    float timer = 0;
    bool goingUp = true;

    void OnValidate()
     {
        int tmp = 1;
        bigLayerValue.TryGetValue(layer.ToString(),out tmp);
        spriteHolder.GetComponent<SpriteRenderer>().sortingOrder = tmp;
        poteau.GetComponent<SpriteRenderer>().sortingOrder = tmp;
     }

    void Start()
    {
        //  foreach (Transform child in transform)
        // {
        //     child.rotation = Quaternion.Euler(0, 90, 0);
        // }
        int tmp = 1;
        bigLayerValue.TryGetValue(layer.ToString(),out tmp);
        spriteHolder.GetComponent<SpriteRenderer>().sortingOrder = tmp;
        poteau.GetComponent<SpriteRenderer>().sortingOrder = tmp;
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
        if (isMoving)
            MoveUpDown();
        DirectionSprite();
    }
}
