using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    // Rigidbody rBody;
    public int speed;
    // Start is called before the first frame update
    void Start()
    {
        // rBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // void FixedUpdate(){
    //     Vector3 pos = rBody.position;
    //     rBody.position += Vector3.back * speed * Time.fixedDeltaTime;
    //     rBody.MovePosition(pos);
    // }
}
