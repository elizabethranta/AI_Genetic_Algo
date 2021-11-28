using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other){
        Debug.Log("Walker killed");
        other.GetComponentInParent<Walker>().Disable();
    }
}
