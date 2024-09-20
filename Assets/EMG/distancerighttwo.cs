using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distancerighttwo : MonoBehaviour
{
    public GameObject Disrighttwo;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.transform.position, Disrighttwo.transform.position) < 0.2f)
        {
            Disrighttwo.SetActive(true);
        }
        else
        {
            Disrighttwo.SetActive(false);
        }
    }
}
