using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainCharController : MonoBehaviour
{
    public enum clickType {neutral, move, talk};
    public clickType currentCT = clickType.neutral;
    public float speed = 5;
    
    public GameObject spriteHolder;
    float lastXPos = 0;

    Rigidbody2D rb;

    Vector3 targetPos;
    Vector3 direction;
    bool isMoving = false;

    public GameObject upWall;
    public GameObject downWall;
    float mouseYMax;
    float mouseYMin;

    Vector3 mouseDir;
    Vector3 mousePoint;

    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        mouseYMax = upWall.transform.position.y - 0.5f - 2.5f;
        mouseYMin = downWall.transform.position.y + 0.5f + 2.5f;
    }


    void getMousePos()
    {
        Vector3 mousePos = Input.mousePosition;
        //Debug.Log(mousePos);
        mousePoint = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Mathf.Abs(cam.transform.position.z)));
        //Debug.Log(mousePoint);
       
    }

    void DirectionSprite()
    {
        float rotaY = 0;
        if (direction.x > 0)
        {
            rotaY = 180;
        }
        spriteHolder.transform.rotation = Quaternion.Euler(0, rotaY, 0);
    }

    void checkClicktype()
    {
        currentCT = clickType.move;
    }

    void MovePrep()
    {
        targetPos = mousePoint;
        targetPos.z = 0;
        if (targetPos.y > mouseYMax)
            targetPos.y = mouseYMax;
        else if (targetPos.y < mouseYMin)
            targetPos.y = mouseYMin;
        Debug.DrawLine(transform.position, targetPos ,Color.red, 1f);
        direction = targetPos - transform.position;
        direction.Normalize();
        DirectionSprite();
        isMoving = true;
    }

    void MouseLeftClick()
    {
        getMousePos();
        checkClicktype();
        switch(currentCT)
        {
            case clickType.move:
            {
                MovePrep();
                break;
            }
            default:
                 break;
        }
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0))
        {
            MouseLeftClick();
        }
    }

    void MoveChar()
    {
        Vector3 nextPos = new Vector3(transform.position.x + direction.x * speed * Time.fixedDeltaTime,
        transform.position.y + direction.y * speed * Time.fixedDeltaTime, 0);
        rb.MovePosition(nextPos);
        if (Vector3.Distance(transform.position, targetPos) < 0.1)
            isMoving = false;
    }

    private void FixedUpdate() {
        if (isMoving == true)
            MoveChar();
    }

    // private void OnCollisionStay2D(Collision2D other) 
    // {
    //     if (other.gameObject.tag == "Wall")
    //     {
    //         targetPos.y = transform.position.y;
    //     }
    // }

}
