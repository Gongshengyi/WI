using UnityEngine;

public class TriggerAnimationRight1 : MonoBehaviour
{
    public GameObject objectC; 
    private float stayTimer;
    private void Update()
    {
        stayTimer += Time.deltaTime;
        if (stayTimer >= 0.2f)
        {
            objectC.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.transform.name);
        if (other.transform.name == "Collider")
        {
            stayTimer = 0;
            objectC.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.name == "Collider")
        {
            objectC.SetActive(false);
        }
    }
}
