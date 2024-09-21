using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// Temporary Script for the light.
/// </summary>
public class RotationController : MonoBehaviour
{
    
    private Transform _transform;
    private float _locationValue;
    
    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        _locationValue += Time.deltaTime;
        if (_locationValue >= 2 * math.PI)
        {
            _locationValue -= 2 * math.PI;
        }

        _transform.position = new Vector3(math.sin(_locationValue) * 3, 0, 0);
    }
}
