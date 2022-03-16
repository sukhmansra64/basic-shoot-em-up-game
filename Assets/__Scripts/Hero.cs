using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    //sets the fields for the class
    static public Hero S;

    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
    public Bounds bounds;

    //sets the bounds for the object and sets the hero game object itself
    //as a singleton
    void Awake()
    {
        S = this;
        bounds = BoundsCheck.CombineBoundsOfChildren(this.gameObject);
        
    }

    // Update is called once per frame from which it gets the input and
    //changes the velocity of the hero accordingly, also checks if the 
    //object is within bounds, and if it isn't it will not allow it to move
    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;

        bounds.center = transform.position;

        Vector3 off = BoundsCheck.ScreenBoundsCheck(bounds, BoundsTest.onScreen);

        if(off != Vector3.zero)
        {
            pos -= off;
            transform.position = pos;
        }

        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);
    }

    //on trigger function which destroys both the opposing game object 
    //and the hero object
    void OnTriggerEnter(Collider other)
    {
        GameObject go = FindTaggedParent(other.gameObject);
        if(go != null){
            Destroy(go);
            Destroy(this.gameObject);
        }else{
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        
    }

    //iterates through the gameobject hierarchy to find the tag
    public static GameObject FindTaggedParent(GameObject go)
    {
        if(go.tag != "Untagged"){
            return go;
        }
        if(go.transform.parent == null){
            return null;
        }
        return FindTaggedParent(go.transform.parent.gameObject);
    }
}
