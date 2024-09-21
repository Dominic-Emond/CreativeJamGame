using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCharacterController : MonoBehaviour
{
    [Header("Movement Dials")]
    [SerializeField] private int moveSpeed = 5;

    [SerializeField] private int jumpStrength = 5;
    
    private Rigidbody2D _rigidBody;

    void Start()
    {
        //Initializing Variables
        _rigidBody = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        //Basic Movement. Change for Input Map and smoother movement. Also Change Velocity Instead?
        Vector3 movement = new Vector3(0, 0, 0);
        
        if (Input.GetKey(KeyCode.A)) //Left
        {
            movement.x -= moveSpeed * Time.fixedDeltaTime;
        }

        if (Input.GetKey(KeyCode.D)) //Right
        {
            movement.x += moveSpeed * Time.fixedDeltaTime;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }

        transform.position += movement;
    }

    private void Jump()
    {
      _rigidBody.velocity = new Vector2(0, jumpStrength);
    }
}
