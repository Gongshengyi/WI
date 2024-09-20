using UnityEngine;

public class TriggerAnimationLeft1 : MonoBehaviour
{
    public GameObject objectA; 
    private float stayTimer;
    private void Update()
    {
        stayTimer += Time.deltaTime;
        if (stayTimer >= 0.2f)
        {
            objectA.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.transform.name);
        if (other.transform.name == "Collider")
        {
            stayTimer = 0;
            objectA.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.name == "Collider")
        {
            objectA.SetActive(false);
        }
    }
}
