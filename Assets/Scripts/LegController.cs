using UnityEngine;

public class LegController : MonoBehaviour
{
    public DistanceJoint2D spring;
    private float contracted;
    private float relaxed;


    [Range(-1f, +1f)] //Restricts position between -1 and 1
    public float position = +1;

    //Called when created
    void Start()
    {
        float distance = spring.distance;
        relaxed = distance * 1.5f;
        contracted = distance / 2f;
    }

    //Move leg on fixed update
    void FixedUpdate()
    {
        spring.distance = linearInterpolation(-1, +1, contracted, relaxed, position);
    }

    //Math
    public static float linearInterpolation(float x0, float x1, float y0, float y1, float x)
    {
        float d = x1 - x0;
        if (d == 0)
            return (y0 + y1) / 2;
        return y0 + (x - x0) * (y1 - y0) / d;
    }
}
