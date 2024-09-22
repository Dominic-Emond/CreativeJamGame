using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FlickingPlatform : MonoBehaviour
{
    private SpriteRenderer spRend;
    [SerializeField] private float flickeringTime = 1.5f;

    [SerializeField] private bool disappered = false;
    [SerializeField] private float timer = 0.0f;
    [SerializeField] private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (!disappered && timer >= flickeringTime)
        {
            spRend.enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            disappered = true;
            timer = 0.0f;
        }

        if(disappered && timer >= flickeringTime)
        {
            spRend.enabled = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            disappered = false;
            timer = 0.0f;
        }

    }
}
