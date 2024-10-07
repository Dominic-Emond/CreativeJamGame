using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class ImprovedCharacterController : MonoBehaviour
{
    //References
    private Movement movementInput;
    private Rigidbody2D _rigidBody;
    private PlayerStats _stats;

    private void Awake()
    {
        movementInput = new Movement();
        movementInput.Enable();
    }
    
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _stats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Input
        //Gather Input in Update (!)
        _stats.playerInput.x = movementInput.CharacterInput.Horizontal.ReadValue<float>();
        
        //Horizontal Movement
        Vector2 movement = new Vector2();
        
        if (math.abs(_stats.playerInput.x) < 0.1f) //Stop moving
        {
            movement.x = Mathf.MoveTowards(_stats.speedLastUpdate.x, 0, _stats.decceleration * Time.fixedDeltaTime);
        }
        else //Moving
        {
            int direction = _stats.playerInput.x > 0 ? 1 : -1;
            movement.x = Mathf.MoveTowards(_stats.speedLastUpdate.x, _stats.maxSpeed * direction, _stats.acceleration * Time.fixedDeltaTime);
        }
        
        _rigidBody.velocity = movement;
        _stats.speedLastUpdate = movement;
    }
}
