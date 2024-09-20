using UnityEngine;

public class TriggerAnimationLeft2 : MonoBehaviour
{
    public GameObject objectB; 
    private float stayTimer;
    private void Update()
    {
        stayTimer += Time.deltaTime;
        if (stayTimer >= 0.2f)
        {
            objectB.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.transform.name);
        if (other.transform.name == "Collider")
        {
            stayTimer = 0;
            objectB.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.name == "Collider")
        {
            objectB.SetActive(false);
        }
    }
}
