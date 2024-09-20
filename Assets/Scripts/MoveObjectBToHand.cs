using System.Collections;

using System.Collections.Generic;

using UnityEngine;




public class MoveObjectBToHand : MonoBehaviour

{

    // Start is called before the first frame update

    public GameObject objectA;

    public GameObject objectB;

    public float duration = 10.0f;


    private Vector3 startPosition;

    private bool isMoving = false;

    private float elapsedTime = 0f;



    void Start()

    {

        if (objectA == null || objectB == null)

        {

            Debug.LogError("ObjectA");

        }

        else

        {

            startPosition = objectB.transform.position;

        }

    }
}


//    void Update()

//    {

//        var interactable = objectA.GetComponent<Interactable>();

//        if (interactable != null && interactable.attachedToHand != null)

//        {

//            hand = interactable.attachedToHand;

//            if (!isMoving)

//            {

//                isMoving = true;

//                elapsedTime = 0f;

//            }

//        }

//        else

//        {

//            hand = null;

//        }





//        if (isMoving)

//        {

//            elapsedTime += Time.deltaTime;

//            float t = elapsedTime / duration;



//            objectB.transform.position = Vector3.Lerp(startPosition, hand.transform.position, t);



//            if (t >= 1f)

//            {

//                isMoving = false;

//            }

//        }

//    }

//}