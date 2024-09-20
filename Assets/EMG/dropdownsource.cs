using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropdownsource : MonoBehaviour
{
    public int times;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        times++;
        this.transform.parent.transform.tag = "Righttwodrop";
        if (times == 2)
        {
            Destroy(this);
        }
    }
}
