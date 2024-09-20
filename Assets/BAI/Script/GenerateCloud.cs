using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionTrigger1 : MonoBehaviour
{
    public ManagePool pool; // object pool
    public float spawnInterval = 3f; // Generation interval (seconds)
    public Vector3 spawnPosition = new Vector3(0, 0, 70); // birth location
    public Vector3 endPosition = new Vector3(0, 0, -100); // Move End (Death Position)
    public float moveSpeed = 5f; // Speed

    private void Start()
    {
        StartCoroutine(SpawnAndMoveCloudSection());
    }

    private IEnumerator SpawnAndMoveCloudSection()
    {
        while (true)
        {
            // Getting objects from the object pool
            GameObject cloudSection = pool.GetObject();
            cloudSection.transform.position = spawnPosition;

            // Start moving
            StartCoroutine(MoveObject(cloudSection));

            // Wait the generation interval
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private IEnumerator MoveObject(GameObject cloudSection)
    {
        while (cloudSection.transform.position != endPosition)
        {
            cloudSection.transform.position = Vector3.MoveTowards(cloudSection.transform.position, endPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // When the move is complete, return the object to the object pool
        pool.ReturnObject(cloudSection);
    }
}
