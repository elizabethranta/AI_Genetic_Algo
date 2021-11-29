using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour
{
    public GameObject body;
    public GameObject itself;
    public LegController left;
    public LegController right;
    public Chromosome chromosome;
    public float offset;
    public GameObject runnerObject;
    private GameRunner runner;
    public WalkerHead head;
    private int achievedTime = 0;
    public AudioClip audio;

    //Each leg, (m, M, o, P) are each gene
    [System.Serializable]
    public struct LegGenes
    {
        public float m; //low limit of range
        public float M; //Upper limit of range
        public float o; //Offset of the wave
        public float p; //Period

        // Based on this equation:
        // https://www.alanzucconi.com/wp-content/ql-cache/quicklatex.com-825734395cc458c8dcf43adfeb560bda_l3.svg
        public float EvaluateAt(float time)
        {
            return (M - m) / 2 * (1 + Mathf.Sin((time + o) * Mathf.PI * 2 / p)) + m;
        }

        //Clone the genes in the leg
        public LegGenes Clone()
        {
            LegGenes leg = new LegGenes();
            leg.m = m;
            leg.M = M;
            leg.o = o;
            leg.p = p;
            return leg;
        }

        //Mutates a gene at random by a random amount
        //Clamps new vlaue to be between -1 and 1
        public void Mutate()
        {
            switch (Random.Range(0, 3))
            {
                case 0:
                    m += Random.Range(-0.1f, 0.1f);
                    // m = Mathf.Clamp(m, -1f, +1f);
                    break;
                case 1:
                    M += Random.Range(-0.1f, 0.1f);
                    // M = Mathf.Clamp(M, -1f, +1f);
                    break;
                case 2:
                    p += Random.Range(-0.25f, 0.25f);
                    // p = Mathf.Clamp(p, 0.1f, 2f);
                    break;
                case 3:
                    o += Random.Range(-0.25f, 0.25f);
                    // o = Mathf.Clamp(o, -2f, 2f);
                    break;
            }
        }

        //Generate random genes
        public void RandomGenes()
        {
            m = Random.Range(-1f, 1f);
            M = Random.Range(-1f, 1f);
            p = Random.Range(-1f, 1f);
            o = Random.Range(0, 1f);
        }
    }

    //Store left and right leg into a single struct
    public struct Chromosome
    {
        public LegGenes left;
        public LegGenes right;

        //Used to mutate all the genes
        public void Mutate()
        {
            //Mutates right or left leg
            if (Random.Range(0f, 1f) > 0.5f)
            {
                left.Mutate();
            }
            else
            {
                right.Mutate();
            }
        }
    }

    //Return a clone of the Walkers chromosome
    public Chromosome Clone()
    {
        Chromosome newChromosome = new Chromosome();
        newChromosome.left = chromosome.left.Clone();
        newChromosome.right = chromosome.right.Clone();
        return newChromosome;
    }

    //Called when Unity starts
    public void Start()
    {
        //Prevents walkers from colliding with eachother and themselves
        Physics2D.IgnoreLayerCollision(3, 3);
        runner = GameObject.Find("AlgoRunner").GetComponent<GameRunner>();
    }

    public void RandomChromosome()
    {
        chromosome.left.m = 0f;
        chromosome.left.M = .5f;
        chromosome.left.o = .5f;
        chromosome.left.p = 1f;

        chromosome.right.m = 0f;
        chromosome.right.M = .75f;
        chromosome.right.o = .5f;
        chromosome.right.p = 1f;
    }

    //Returns the score of the walker, if it hit its head, half the score
    public float GetScore()
    {
        return achievedTime;
    }

    //Called each frame update
    public void Update()
    {
        left.position = chromosome.left.EvaluateAt(Time.time) - offset;
        right.position = chromosome.right.EvaluateAt(Time.time) - offset;
        achievedTime = runner.time;
    }

    public bool IsAlive()
    {
        return itself.activeSelf;
    }

    public void ToogleEnable(bool boolean)
    {
        itself.SetActive(boolean);
    }

    public void LavaPitted()
    {
        AudioSource.PlayClipAtPoint(audio, this.gameObject.transform.position);
        itself.SetActive(false);
    }

    public void BestColor(){
        SpriteRenderer renderer = body.GetComponent<SpriteRenderer>();
        renderer.color = Color.red;
        renderer.sortingOrder = 32766;
    }
}
