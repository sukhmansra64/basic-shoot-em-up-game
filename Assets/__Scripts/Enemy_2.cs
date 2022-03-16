using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : Enemy
{ 
    //initiate the fields
    public bool posX;
    public Bounds otherBounds;

    //sets the bounds for the enemy, and checks if the object is off the screen
    //repeatedly from when the object is initialized, also randomly assigns
    //direction using the posX boolean
    void Awake()
    {
        otherBounds = BoundsCheck.CombineBoundsOfChildren(this.gameObject);
        InvokeRepeating("CheckOffscreen", 0f, 2f);
        int initX = Random.Range(0,2);
        if (initX == 0)
        {
            posX = true;
        }
        else
        {
            posX = false;
        }
    }

    //moves on each new frame
    void Update()
    {
        Move();
    }

    //overrides the move method which checks if the object is in bounds, and if 
    //it goes out of bounds on the x-axis, it will change direction
    public override void Move()
    {
        Vector3 tempPos = pos;
        otherBounds.center = transform.position;

        Vector3 off = BoundsCheck.ScreenBoundsCheck(otherBounds, BoundsTest.onScreen);

        if (off.x != 0)
        {
            posX = !posX;
        }
        tempPos.y -= speed * Time.deltaTime;
    if (posX)
    {
        tempPos.x -= speed * Time.deltaTime;
    }
    else {
        tempPos.x += speed * Time.deltaTime;
    }
        pos = tempPos;
    }

    //checks if the gameobject left the bounds on the y-axis, and if it has,
    //the game object will be destroyed
    void CheckOffscreen()
    {
        if (bounds.size == Vector3.zero)
        {
            bounds = BoundsCheck.CombineBoundsOfChildren(this.gameObject);
            boundsCenterOffset = bounds.center - transform.position;
        }

        bounds.center = transform.position + boundsCenterOffset;
        Vector3 off = BoundsCheck.ScreenBoundsCheck(bounds, BoundsTest.offScreen);
        if (off != Vector3.zero)
        {
            if (off.y < 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
