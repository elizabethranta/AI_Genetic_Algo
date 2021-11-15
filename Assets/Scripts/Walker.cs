using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker
{
    public LegController left;
    public LegController right;
    public Chromosome chromosome;

    //Each leg, (m, M, o, P) are each gene
    [System.Serializable]
    public struct GenomeLeg{
        public float m;
        public float M;
        public float o;
        public float p;

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

    //Called each frame update
    public void Update ()
    {
        left.position =  chromosome.left.EvaluateAt(Time.time);
        right.position = chromosome.right.EvaluateAt(Time.time);
    }
}
