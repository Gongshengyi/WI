using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingAverageFilter
{
    private Queue<float> window = new Queue<float>();
    private int windowSize;
    private float sum;

    public MovingAverageFilter(int size)
    {
        windowSize = size;
    }
    public float Filter(float newValue)
    {
        window.Enqueue(newValue);
        sum += newValue;

        if(window.Count > windowSize)
        {
            sum -= window.Dequeue();
        }
        return sum / window.Count;
    }
}
