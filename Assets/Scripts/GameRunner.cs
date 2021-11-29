using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameRunner : MonoBehaviour
{
    public Text timeText;
    public Text generationText;
    public Text prevBestScoreText;
    public float maxTimeout = 30f;
    public int time = 0;
    public int generations = 100;
    public int currentGeneration = 1;
    public float bestScore = 0;
    public int populationSize = 50;
    private Walker bestWalker;
    public GameObject prefab;

    private List<Walker> walkers = new List<Walker>();

    //https://docs.unity3d.com/ScriptReference/MonoBehaviour.StartCoroutine.html


    // Start is called before the first frame update
    void Start()
    {
        bestWalker = GenerateFirstWalker();
        StartCoroutine(SimulationLoop());
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        timeText.text = "Time: " + time++;
    }

    private Walker GenerateFirstWalker()
    {
        Walker temp = new Walker();
        temp.RandomChromosome();
        return temp;
    }

    private IEnumerator SimulationLoop()
    {
        for (int i = 0; i < generations; i++)
        {
            CreateWalkers();
            generationText.text = "Generation: " + currentGeneration++.ToString();
            prevBestScoreText.text = "Prev Best Score: " + bestScore.ToString();
            time = 0;
            StartSimulation();
            // yield return new WaitForSeconds(maxTimeout);
            while (!AllDead())
            {
                yield return null;
            }
            StopSimulation();

            EvaluateScore();
            DestroyCreatures();

            yield return new WaitForSeconds(1);
        }
    }

    private bool AllDead()
    {
        bool allDead = true;
        foreach (Walker walker in walkers)
        {
            if (walker.IsAlive())
                allDead = false;
        }
        return allDead;
    }

    public void EvaluateScore()
    {
        foreach (Walker walker in walkers)
        {
            float score = walker.GetScore();
            if (score > bestScore)
            {
                bestScore = score;
                bestWalker.chromosome = walker.Clone();
            }
        }
    }

    //Create a population
    public void CreateWalkers()
    {
        Vector3 pos = Vector3.zero;
        //Runs one first generations
        if (bestScore == 0)
        {
            for (int i = 0; i < populationSize; i++)
            {
                //Creates a Gameobject in unity
                Walker walker = Instantiate(prefab, pos, Quaternion.identity).GetComponent<Walker>();

                //Creates the chromosome based off the best walker from the last generation and mutate it
                Walker.Chromosome tempChromosome = bestWalker.Clone();
                tempChromosome.left.RandomGenes();
                tempChromosome.right.RandomGenes();

                //Assign the mutated walker to the newly created one
                walker.chromosome = tempChromosome;

                walker.ToogleEnable(false);
                walkers.Add(walker);
            }
        }
        else
        {
            for (int i = 0; i < populationSize/2; i++)
            {
                //Creates a Gameobject in unity
                Walker walker = Instantiate(prefab, pos, Quaternion.identity).GetComponent<Walker>();

                //Creates the chromosome based off the best walker from the last generation and mutate it
                Walker.Chromosome tempChromosome = bestWalker.Clone();
                tempChromosome.Mutate();

                //Assign the mutated walker to the newly created one
                walker.chromosome = tempChromosome;
                // Debug.Log("Left m: " + walker.chromosome.left.m);
                walker.ToogleEnable(false);
                walkers.Add(walker);
            }
            //Add some new random ones for variaty
            for (int i = 0; i < populationSize/2; i++)
            {
                //Creates a Gameobject in unity
                Walker walker = Instantiate(prefab, pos, Quaternion.identity).GetComponent<Walker>();

                //Creates the chromosome based off the best walker from the last generation and mutate it
                Walker.Chromosome tempChromosome = bestWalker.Clone();
                tempChromosome.left.RandomGenes();
                tempChromosome.right.RandomGenes();

                //Assign the mutated walker to the newly created one
                walker.chromosome = tempChromosome;

                walker.ToogleEnable(false);
                walkers.Add(walker);
            }
            //Add best walker to generation and give fancy color
            {
                Walker walker = Instantiate(prefab, pos, Quaternion.identity).GetComponent<Walker>();
                Walker.Chromosome tempChromosome = bestWalker.Clone();
                walker.chromosome = bestWalker.chromosome;
                walker.BestColor();
                walkers.Add(walker);
            }
        }
    }

    //Enables all the walkers
    public void StartSimulation()
    {
        foreach (Walker walker in walkers)
            walker.ToogleEnable(true);
    }

    //Disables all the walkers
    public void StopSimulation()
    {
        foreach (Walker walker in walkers)
            walker.ToogleEnable(false);
    }

    //Destroy all the walkers
    public void DestroyCreatures()
    {
        //Destroy each of the unity objects
        foreach (Walker walker in walkers)
            Destroy(walker.gameObject);
        //Empty the list
        walkers.Clear();
    }
}
