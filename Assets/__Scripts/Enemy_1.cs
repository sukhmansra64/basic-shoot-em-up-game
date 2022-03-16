using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : Enemy
{
    //on every frame, the enemy will call the inherited move method
    void Update()
    {
        Move();
    }
}
