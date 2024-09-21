using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// Character Controller, takes care of the player movement.
/// </summary>
public class CustomCharacterController : MonoBehaviour
{
    [Header("Movement Dials")]
    [SerializeField] private int moveSpeed = 5;
    [SerializeField] private int jumpStrength = 5;
    [SerializeField] private float raycastLength = 0.2f;

    [Header("Ability Parameters")] 
    public float abilityMaximumTime = 2f;
    public float cooldownTime = 2f;
    public GameObject uiAbility;

    [Header("Character Controls")] 
    public InputAction movementControls;
    public InputAction jumpControls;
    public InputAction abilityControls;
    
    [Header("Misc")]
    public Color _disappearColor;
    
    //Private fields
    //Components
    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRenderer;
    
    //Jump
    private bool _isGrounded;
    private bool _hasReleasedJump;
    
    //Ability
    private bool _isDisappeared;
    private float _abilityLockTime;
    private float _abilityTimeLeft;
    private Color _normalColor;
    private Image _abilityImage;

    void Start()
    {
        //Initializing Variables
        _isGrounded = true;
        _hasReleasedJump = true;
        _isDisappeared = false;
        _abilityLockTime = 0f;
        _abilityTimeLeft = 0f;
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _normalColor = _spriteRenderer.color;

        if (uiAbility == null)
        {
            Debug.LogWarning("No UI attached to character!");
        }
        else
        {
            _abilityImage = uiAbility.GetComponent<Image>();
        }
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

    void FixedUpdate()
    {
        //Vector for character movement
        //X value based on player Input
        Vector2 movement = new Vector2();
        movement.x = movementControls.ReadValue<float>() * Time.fixedDeltaTime * moveSpeed;
        _isGrounded = IsGrounded();
        
        //Y mixed between Jumping and Gravity
        //Change to InputActions (!)
        if (jumpControls.IsPressed() && _isGrounded && _hasReleasedJump)
        {
            _hasReleasedJump = false;
            movement.y = jumpStrength;
        }
        else
        {
            if (!jumpControls.IsPressed()) _hasReleasedJump = true;
            movement.y = _rigidBody.velocity.y;
        }
        
        //Ability Pressed. Overwrites Everything.
        if (_isDisappeared)
        {
            movement = new Vector2(0, 0);
        }
        
        //Disappearance Ability
        //if(controls.)
        
        _rigidBody.velocity = movement;
    }

    void Update()
    {
        if (!Mathf.Approximately(_abilityLockTime, 0f))
        {
            _abilityLockTime = math.max(_abilityLockTime - Time.deltaTime, 0f);
            if (Mathf.Approximately(_abilityLockTime, 0f)) SetUIAbility();
        }

        if (!Mathf.Approximately(_abilityTimeLeft, 0f))
        {
            _abilityTimeLeft = math.max(_abilityTimeLeft - Time.deltaTime, 0f);
            
        }
        
        // Character Appears/Disappears
        if (abilityControls.WasPressedThisFrame() && Mathf.Approximately(_abilityLockTime, 0f) && !_isDisappeared)
        {
            CharacterDisappears();
        }
        else if ((abilityControls.WasReleasedThisFrame() || Mathf.Approximately(_abilityTimeLeft, 0f)) && _isDisappeared)
        {
            CharacterAppears();
        }
        
    }

    /// <summary>
    /// Raycast to check if character is grounded.
    /// </summary>
    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastLength, LayerMask.GetMask("Ground"));

        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }


    /// <summary>
    /// Character Disappears (Ability Triggered)
    /// </summary>
    void CharacterDisappears()
    {
        _isDisappeared = true;
        _rigidBody.simulated = false;
        _spriteRenderer.color = _disappearColor;
        _abilityTimeLeft = abilityMaximumTime;
        SetUIAbility(false);
    }

    /// <summary>
    /// Character Reappears
    /// </summary>
    void CharacterAppears()
    {
        _isDisappeared = false;
        _rigidBody.simulated = true;
        _spriteRenderer.color = _normalColor;
        _abilityLockTime = cooldownTime;
    }

    void SetUIAbility(bool available = true)
    {
        if (available) _abilityImage.color = new Color(1f, 1f, 1f, 1f);
        else _abilityImage.color = new Color(1f, 1f, 1f, 0f);
    }
}
