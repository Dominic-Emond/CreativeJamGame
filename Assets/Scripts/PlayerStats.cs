using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Input")] 
    public Vector2 playerInput = Vector2.zero;
    public float horizontalThreshold = 0.1f;
    
    [Header("Horizontal Movement")] 
    public float maxSpeed = 8f;
    public float acceleration = 24f;
    public float decceleration = 12f;

    [Header("Horizontal Values. Do not Edit")]
    public Vector2 speedLastUpdate = Vector2.zero;
}
