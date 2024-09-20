using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other) // When XR Origin touches the trigger, switch scenes
    {
        if (other.CompareTag("XR Origin"))
        {
            SceneManager.LoadScene("TTtest1");
        }
    }

}
