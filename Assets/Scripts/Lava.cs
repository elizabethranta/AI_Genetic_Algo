using UnityEngine;

public class Lava : MonoBehaviour
{
    //Called when another collider hits the lava collider
    void OnTriggerEnter2D(Collider2D other){
            //Tries to get walker componenet from other object
            Walker walker = other.GetComponentInParent<Walker>();
            //If it is a walker, disable it
            if(walker != null)
                walker.LavaPitted();
    }
}
