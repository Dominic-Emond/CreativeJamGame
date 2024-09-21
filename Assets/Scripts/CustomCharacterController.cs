using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

/// <summary>
/// Character Controller, takes care of the player movement.
/// </summary>
public class CustomCharacterController : MonoBehaviour
{
    [Header("Movement Dials")]
    [SerializeField] private int moveSpeed = 5;

    [SerializeField] private int jumpStrength = 5;

    [Header("Character Controls")] 
    public InputAction controls;
    
    
    //Private fields
    private Rigidbody2D _rigidBody;
    private bool _isGrounded;

    void Start()
    {
        //Initializing Variables
        _isGrounded = true; // (!) Change
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// When GameObject is enabled, starts the InputAction Controls.
    /// Important to avoid issue (?)
    /// </summary>
    private void OnEnable()
    {
        controls.Enable();
    }

    /// <summary>
    /// When GameObject is disabled, disables the InputAction Controls.
    /// </summary>
    private void OnDisable()
    {
        controls.Disable();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _isGrounded = true;
        }
    }


    void FixedUpdate()
    {
        //Vector for character movement
        //X value based on player Input
        Vector2 movement = new Vector2();
        movement.x = controls.ReadValue<Vector2>().x * Time.fixedDeltaTime * moveSpeed;
        
        //Y mixed between Jumping and Gravity
        if (Input.GetKey(KeyCode.Space) && _isGrounded)
        {
            _isGrounded = false;
            movement.y = jumpStrength;
        }
        else
        {
            movement.y = _rigidBody.velocity.y;
        }
        
        _rigidBody.velocity = movement;
    }
}
