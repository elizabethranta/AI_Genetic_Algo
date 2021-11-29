using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other){
            Walker walker = other.GetComponentInParent<Walker>();
            if(walker != null)
                walker.LavaPitted();
    }
}
