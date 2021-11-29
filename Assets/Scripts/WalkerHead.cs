using UnityEngine;

public class WalkerHead : MonoBehaviour
{
    public Walker walker;

    //If it lands on its head, disable it
    void OnCollisionEnter2D(Collision2D other){
        walker.ToogleEnable(false);
    }
}
