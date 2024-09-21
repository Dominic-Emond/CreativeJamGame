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
    public InputAction movementControls;
    public InputAction jumpControls;
    public InputAction abilityControls;
    
    [Header("Misc")]
    public Color _disappearColor;
    
    //Private fields
    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRenderer;
    private bool _isGrounded;
    private bool _isDisappeared;
    private Color _normalColor;

    void Start()
    {
        //Initializing Variables
        _isGrounded = true; // (!) Change
        _isDisappeared = false;
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _normalColor = _spriteRenderer.color;
    }

    /// <summary>
    /// When GameObject is enabled, starts the InputAction Controls.
    /// Important to avoid issues
    /// </summary>
    private void OnEnable()
    {
        movementControls.Enable();
        jumpControls.Enable();
        abilityControls.Enable();
    }

    /// <summary>
    /// When GameObject is disabled, disables the InputAction Controls.
    /// </summary>
    private void OnDisable()
    {
        movementControls.Disable();
        jumpControls.Disable();
        abilityControls.Disable();
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
        movement.x = movementControls.ReadValue<float>() * Time.fixedDeltaTime * moveSpeed;
        
        //Y mixed between Jumping and Gravity
        //Change to InputActions (!)
        if (jumpControls.IsPressed() && _isGrounded)
        {
            _isGrounded = false;
            movement.y = jumpStrength;
        }
        else
        {
            movement.y = _rigidBody.velocity.y;
        }
        
        //Ability Pressed;
        if (abilityControls.IsPressed() || _isDisappeared)
        {
            movement.y = 0;
            CharacterDisappears();
        }
        
        //Disappearance Ability
        //if(controls.)
        
        _rigidBody.velocity = movement;
    }

    void Update()
    {
        //Disapeared.
        if (abilityControls.WasReleasedThisFrame())
        {
            CharacterAppears();
        }
    }


    /// <summary>
    /// Character Disappears (Ability Triggered)
    /// </summary>
    void CharacterDisappears()
    {
        _isDisappeared = true;
        _rigidBody.simulated = false;
        _spriteRenderer.color = _disappearColor;
        // Change isGrounded?
    }

    /// <summary>
    /// Character Reappears
    /// </summary>
    void CharacterAppears()
    {
        _isDisappeared = false;
        _rigidBody.simulated = true;
        _spriteRenderer.color = _normalColor;
    }
}
