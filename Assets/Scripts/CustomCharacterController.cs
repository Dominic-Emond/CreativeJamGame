using Unity.Mathematics;
using UnityEngine;
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
    [SerializeField] private float dampingFactor = 0.5f;

    [Header("Ability Parameters")] 
    public float abilityMaximumTime = 2f;
    public float cooldownTime = 2f;
    public GameObject uiAbility;
    
    [Header("Dash")]
    public float dashSpeed = 2f;
    public float dashDuration = 1f;
    public float dashCooldownTime = 2f;

    [Header("Character Controls")] 
    public InputAction movementControls;
    public InputAction jumpControls;
    public InputAction abilityControls;
    public InputAction dashControls;
    public InputAction dashVerticalControls;
    
    [Header("Misc")]
    public Color _disappearColor;
    
    //Private fields
    //Components
    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private Collider2D _collider;
    
    //Jump
    private bool _isGrounded;
    private bool _hasReleasedJump;
    
    //Ability
    private bool _isDisappeared;
    private float _abilityLockTime;
    private float _abilityTimeLeft;
    private Color _normalColor;
    private Image _abilityImage;
    private GameObject _parent;
    private float _gravity;
    
    //Dash
    private float _dashLockTime = 0f;
    private float _dashTimeLeft = 0f;
    private bool _isDashing = false;
    private Vector2 _dashMovement;
    
    //Animation
    private bool _isFlipped;

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
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
        _normalColor = _spriteRenderer.color;
        _parent = transform.parent.gameObject;
        _isFlipped = false;
        _gravity = _rigidBody.gravityScale;

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
        dashControls.Enable();
        dashVerticalControls.Enable();
    }

    /// <summary>
    /// When GameObject is disabled, disables the InputAction Controls.
    /// </summary>
    private void OnDisable()
    {
        movementControls.Disable();
        jumpControls.Disable();
        abilityControls.Disable();
        dashControls.Disable();
        dashVerticalControls.Disable();
    }

    void FixedUpdate()
    {
        //Vector for character movement
        //X value based on player Input
        Vector2 movement = new Vector2();

        if (!Mathf.Approximately(movementControls.ReadValue<float>(), 0f))
        {
            movement.x = movementControls.ReadValue<float>() * Time.fixedDeltaTime * moveSpeed;
        }
        else //Damping
        {
            movement.x = _rigidBody.velocity.x * Mathf.Pow(dampingFactor, Time.fixedDeltaTime);
        }
        
        _isGrounded = IsGrounded();
        _animator.SetFloat("Speed", movementControls.ReadValue<float>());
        
        
        //Flipping character if he is moving to the left.
        if (movementControls.ReadValue<float>() > 0f && _isFlipped)
        {
            _spriteRenderer.flipX = false;
            _isFlipped = false;
        }
        else if (movementControls.ReadValue<float>() < 0f && !_isFlipped)
        {
            _spriteRenderer.flipX = true;
            _isFlipped = true;
        }
        
        //Y mixed between Jumping and Gravity
        //Change to InputActions (!)
        if (jumpControls.IsPressed() && _isGrounded && _hasReleasedJump)
        {
            _animator.SetBool("Jumping", true);
            _hasReleasedJump = false;
            movement.y = jumpStrength;
        }
        else
        {
            if (!jumpControls.IsPressed()) _hasReleasedJump = true;
            movement.y = _rigidBody.velocity.y;
        }
        
        // Dash
        if (_isDisappeared && dashControls.IsPressed() && Mathf.Approximately(_dashLockTime, 0f) && !_isDashing)
        {
            _isDashing = true;
            _dashTimeLeft = dashDuration;
            _dashMovement.x = movement.x * dashSpeed;
            _dashMovement.y = dashVerticalControls.ReadValue<float>() * Time.fixedDeltaTime * moveSpeed * dashSpeed;
            
        }

        if (_isDashing)
        {
            movement = _dashMovement;
        }
        else if (_isDisappeared) //Ability Pressed. Overwrites Everything (Except Dash)
        {
            movement = new Vector2(0, 0);
        }
        
        //Disappearance Ability
        //if(controls.)
        
        _rigidBody.velocity = movement;
    }

    void Update()
    {
        if (_rigidBody.velocity.y < 0)
        {
            _animator.SetBool("Falling", true);
        }
        
        if (!Mathf.Approximately(_abilityLockTime, 0f))
        {
            _abilityLockTime = math.max(_abilityLockTime - Time.deltaTime, 0f);
            if (Mathf.Approximately(_abilityLockTime, 0f)) SetUIAbility();
        }

        if (!Mathf.Approximately(_abilityTimeLeft, 0f))
        {
            _abilityTimeLeft = math.max(_abilityTimeLeft - Time.deltaTime, 0f);
            
        }

        if (!Mathf.Approximately(_dashLockTime, 0f))
        {
            _dashLockTime = math.max(_dashLockTime - Time.deltaTime, 0f);
        }
        
        if (!Mathf.Approximately(_dashTimeLeft, 0f))
        {
            _dashTimeLeft = math.max(_dashTimeLeft - Time.deltaTime, 0f);
            if (Mathf.Approximately(_dashTimeLeft, 0f))
            {
                _isDashing = false;
                _dashLockTime = dashCooldownTime;
            }
        }
        
        // Character Appears/Disappears
        if (abilityControls.WasPressedThisFrame() && Mathf.Approximately(_abilityLockTime, 0f) && !_isDisappeared)
        {
            CharacterDisappears();
        }
        else if ((abilityControls.WasReleasedThisFrame() || Mathf.Approximately(_abilityTimeLeft, 0f)) && _isDisappeared && !_isDashing)
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
            _animator.SetBool("Jumping", false);
            _animator.SetBool("Falling", false);
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
        //_rigidBody.simulated = false;
        _collider.enabled = false;
        _rigidBody.gravityScale = 0f;
        _spriteRenderer.color = _disappearColor;
        _abilityTimeLeft = abilityMaximumTime;
        SetUIAbility(false);
        transform.SetParent(null);
    }

    /// <summary>
    /// Character Reappears
    /// </summary>
    void CharacterAppears()
    {
        _isDisappeared = false;
        //_rigidBody.simulated = true;
        _collider.enabled = true;
        _rigidBody.gravityScale = _gravity;
        _spriteRenderer.color = _normalColor;
        _abilityLockTime = cooldownTime;
        transform.SetParent(_parent.transform);
    }

    void SetUIAbility(bool available = true)
    {
        if (available) _abilityImage.color = new Color(1f, 1f, 1f, 1f);
        else _abilityImage.color = new Color(1f, 1f, 1f, 0f);
    }
}
