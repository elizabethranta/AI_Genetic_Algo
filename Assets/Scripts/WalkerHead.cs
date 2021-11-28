using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerHead : MonoBehaviour
{
    public bool hasHitHead = false;

    
    void OnCollisionEnter2D(Collision2D other){
        hasHitHead = true;
    }
}
