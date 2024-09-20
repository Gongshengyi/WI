using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Time_event : MonoBehaviour
{

    public float delayTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("myFunction",delayTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void myFunction(){
        Debug.Log("function executed");
    }
}
