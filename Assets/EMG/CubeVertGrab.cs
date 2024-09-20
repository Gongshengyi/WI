using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeVertGrab : MonoBehaviour
{
    private Vector3 startPos;
    public GameObject targetGrabCube;
    public float vertLength;

    public float vertScale;
    public float cubeLen;
    public float grabFloat;

    public TouchHandGrabInteractable grabObj;
    public GunFire gunFire;

    private bool goBackStart;
    public MeshRenderer ballMaterial;

    public GameObject handColl;
    private bool ballsound;
    // Start is called before the first frame update
    void Start()
    {
        grabObj=this.GetComponent<TouchHandGrabInteractable>();
        startPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {  

        vertLength = this.transform.position.y;

        if (!float.IsNaN(gunFire.GunFireDisplayedValue))
        {
          
            vertScale = gunFire.GunFireDisplayedValue / 0.110F;
        }
        if (Vector3.Distance(this.transform.position, handColl.transform.position) < 0.2f)
        {
            if (gunFire.GunFireDisplayedValue <= 0.030F)
            {
                if (!ballsound)
                {
                    ballMaterial.GetComponent<AudioSource>().Play();
                    ballsound = true;
                }
                ballMaterial.material.color = Color.green;
            }
            else
            {
                ballMaterial.material.color = Color.red;
            }
        }
        else {
            ballMaterial.material.color = Color.red;
        }
        if (Vector3.Distance(this.transform.position, targetGrabCube.transform.position) < cubeLen)
        {
          
            if (gunFire.GunFireDisplayedValue >= grabFloat)
            {
                grabObj.enabled = true;
            }
            else
            {
                grabObj.enabled=false;
            }
        }
        else
        {
            Debug.Log("else_0.78");
            grabObj.enabled = true;
            vertLength = 0.7811f;
        }
        if (goBackStart)
        {
            if (this.transform.position == startPos)
            {
                    goBackStart = false;
            }
            this.transform.position = startPos;
        }
        vertLength=Mathf.Clamp(vertLength, 0.7811f, 0.9614f-((0.9614F-0.7811F)*(1-vertScale)));
        targetGrabCube.transform.position = new Vector3(startPos.x,vertLength,startPos.z);
    }
    public void syncPos()
    {
        goBackStart = true;
    }
}
