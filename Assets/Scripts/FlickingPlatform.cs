using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FlickingPlatform : MonoBehaviour
{
    private SpriteRenderer spRend;
    [SerializeField] private float moveTime = 1.5f;

    [SerializeField] private bool open = false;
    [SerializeField] private float timer = 0.0f;
    [SerializeField] private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spRend = GetComponent<SpriteRenderer>();
    }
    void animatorSet()
    {
        animator.SetBool("open", open);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (!open && timer >= moveTime)
        {
            //spRend.enabled = false; //for flicking platform
            //gameObject.GetComponent<BoxCollider2D>().enabled = false; no need to set to false. Used dynamic collider
            open = true;
            timer = 0.0f;
        }

        if(open && timer >= moveTime)
        {
            //spRend.enabled = true;
            //gameObject.GetComponent<BoxCollider2D>().enabled = true;
            open = false;
            timer = 0.0f;
        }
        animatorSet();
    }
}
