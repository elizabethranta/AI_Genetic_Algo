using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameRunner : MonoBehaviour
{
    public Text timeText;
    public int time = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate(){
        timeText.text = "Time: " + time++;
    }
}
