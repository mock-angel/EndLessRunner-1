using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float runSpeed = 5f;
    
    private Rigidbody2D rb;
    
    Vector3 movement;
    
    void Start(){
        rb = gameObject.GetComponent<Rigidbody2D>();
        
        movement.x = 1;
        movement.y = 0;
        movement.z = 0;
    }
    
    void Update()
    {
        //Move to fixed update?
//        rb.MovePosition(rb.position + movement * runSpeed * Time.deltaTime);
    }
    
    void FixedUpdate()
    {
        //Move to fixed update?
//        rb.MovePosition(rb.position + movement * runSpeed * Time.fixedDeltaTime);
        transform.position += movement * runSpeed * Time.fixedDeltaTime;
    }
}
