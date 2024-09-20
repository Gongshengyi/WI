using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosTranformer : MonoBehaviour
{
    public GameObject[] playerPos;
    public GameObject targetGameobject;
    private int playerPosIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    public void goNextPos()
    {
        if (playerPosIndex < 4)
        {
            targetGameobject.transform.position = playerPos[playerPosIndex].transform.position;
            playerPosIndex++;
            if (playerPosIndex == 4)
            {
                this.gameObject.SetActive(false);
            }
        }
        else
        {
           
        }
    }
}
