using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //sets fields for the enemy game object
    public float speed = 10f;
    public Bounds bounds;
    public Vector3 boundsCenterOffset;

    //sets the bounds for the enemy, and checks if the object is off the screen
    //repeatedly from when the object is initialized
    void Awake()
    {
        InvokeRepeating("CheckOffscreen", 0f, 2f);
    }

    //A method which can be overriden, but the base version moves the gameobject
    //down along the y-axis
    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }

    //Vector property of the class
    public Vector3 pos
    {
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
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
