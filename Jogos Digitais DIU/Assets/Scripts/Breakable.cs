using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
   
    void OnTriggerEnter(Collider coll){
        // if incoming object is a player attack
        if (coll.CompareTag("PlayerAttack"))
        {
            // Disables the Collider2D component
            Destroy(gameObject);
        }
    }
}

//coll.GetComponent<Collider2D>().CompareTag("PlayerAttack")
