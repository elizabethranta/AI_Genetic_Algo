using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameRunner : MonoBehaviour
{
    public Text timeText;
    public Text generationText;
    public int time = 0;
    public int generations = 100;
    public int currentGeneration = 1;
    public int populationSize = 100;

    private List<Walker> walkers = new List<Walker>();
    
    //https://docs.unity3d.com/ScriptReference/MonoBehaviour.StartCoroutine.html


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

    public void StartSimulation ()
    {
        foreach (Walker walker in walkers)
            walker.enabled = true;
    }
    public void StopSimulation ()
    {
        foreach (Walker walker in walkers)
            walker.enabled = false;
    }
    public void DestroyCreatures ()
    {
        foreach (Walker walker in walkers)
            Destroy(walker.gameObject);
        walkers.Clear();
    }
}
