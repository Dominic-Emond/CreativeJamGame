using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickingPlatform : MonoBehaviour
{
    [SerializeField] private float flickeringTime = 1.5f;

    [SerializeField] private bool disappered = false;
    [SerializeField] private float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (!disappered && timer >= flickeringTime)
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().simulated = false;
            disappered = true;
            timer = 0.0f;
        }

        if(disappered && timer >= flickeringTime)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
            gameObject.GetComponent<Rigidbody2D>().simulated = true;
            disappered = false;
            timer = 0.0f;
        }

    }
}
