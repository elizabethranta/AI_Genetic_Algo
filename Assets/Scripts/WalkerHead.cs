using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerHead : MonoBehaviour
{
    public Walker walker;
    void OnCollisionEnter2D(Collision2D other){
        walker.ToogleEnable(false);
    }
}
