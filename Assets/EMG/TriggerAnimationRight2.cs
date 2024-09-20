using UnityEngine;

public class TriggerAnimationRight2 : MonoBehaviour
{
    public GameObject objectD; 
    private float stayTimer;
    private void Update()
    {
        stayTimer += Time.deltaTime;
        if (stayTimer >= 0.2f)
        {
            objectD.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.transform.name);
        if (other.transform.name == "Collider")
        {
            stayTimer = 0;
            objectD.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.name == "Collider")
        {
            objectD.SetActive(false);
        }
    }
}
