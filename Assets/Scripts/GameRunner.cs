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
    private Walker.Chromosome bestChromosome;
    public GameObject prefab;

    private List<Walker> walkers = new List<Walker>();
    
    //https://docs.unity3d.com/ScriptReference/MonoBehaviour.StartCoroutine.html


    // Start is called before the first frame update
    void Start()
    {
        CreateWalkers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate(){
        timeText.text = "Time: " + time++;
    }

    public void CreateWalkers(){
        Vector3 pos = Vector3.zero;

        for(int i = 0; i < populationSize; i++){
            Walker walker = Instantiate(prefab, pos, Quaternion.identity).GetComponent<Walker>();

            walker.chromosome = bestChromosome;
            walkers.Add(walker);
        }
        Debug.Log(walkers);
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
