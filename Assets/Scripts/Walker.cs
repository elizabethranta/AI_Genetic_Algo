using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour
{
    public LegController left;
    public LegController right;
    public Chromosome chromosome;
    public float offset;

    //Each leg, (m, M, o, P) are each gene
    [System.Serializable]
    public struct GenomeLeg{
        public float m; //low limit of range
        public float M; //Upper limit of range
        public float o; //Offset of the wave
        public float p; //Period

        // Based on this equation:
        // https://www.alanzucconi.com/wp-content/ql-cache/quicklatex.com-825734395cc458c8dcf43adfeb560bda_l3.svg
        public float EvaluateAt(float time){
            return (M - m) / 2 * (1 + Mathf.Sin((time+o) * Mathf.PI * 2 / p)) + m;
        }
    }

    //Store left and right leg into a single struct
    public struct Chromosome {
        public GenomeLeg left;
        public GenomeLeg right;
    }

    public void Start (){
        chromosome.left.m = 0f;
        chromosome.left.M = 3f;
        chromosome.left.o = .5f;
        chromosome.left.p = 1f;

        chromosome.right.m = 0f;
        chromosome.right.M = 3f;
        chromosome.right.o = 1f;
        chromosome.right.p = 1f;
    }

    //Called each frame update
    public void Update ()
    {
        left.position =  chromosome.left.EvaluateAt(Time.time) - offset;
        right.position = chromosome.right.EvaluateAt(Time.time) - offset;
    }
}
