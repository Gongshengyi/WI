using UnityEngine;

public class TriggerArm : MonoBehaviour
{
    public GameObject Arm; 
    private float stayTimer;
    private void Update()
    {
        stayTimer += Time.deltaTime;
        if (stayTimer >= 0.2f)
        {
            Arm.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.transform.name);
        if (other.transform.name == "Collider")
        {
            stayTimer = 0;
            Arm.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.name == "Collider")
        {
            Arm.SetActive(false);
        }
    }
}
