using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject deathDetectionPoint;
    public LayerMask layerMask;
    public PlayerMovement playerMovementScript;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if(!playerMovementScript.stop)
            if(Physics2D.Raycast(deathDetectionPoint.transform.position, Vector2.right, 0.01f, layerMask)){
                //Stop all physics operations on player
                GetComponent<Rigidbody2D>().isKinematic = true;
                playerMovementScript.stop = true;
                
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    //            Destroy(GetComponent<Rigidbody2D>());
            }
    }
}
