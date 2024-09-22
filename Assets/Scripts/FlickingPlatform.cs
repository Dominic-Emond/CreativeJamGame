using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FlickingPlatform : MonoBehaviour
{
    private SpriteRenderer spRend;
    //[SerializeField] private float stopTime = 1.5f;
    //[SerializeField] private float animationTime = 1.0f; //wait animation to finish
    [SerializeField] private bool open = false;
    [SerializeField] private float timer = 0.0f;
    [SerializeField] private float waitTime = 1.5f;
    //[SerializeField] private bool animationFinished = true;
    //[SerializeField] private float stopTimer = 0.0f;
    //[SerializeField] private float animationTimer = 0.0f;
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
        //if (animationFinished)
        //{
        //    stopTimer += Time.deltaTime;
        //}

        timer += Time.deltaTime;

        if (!open && timer >= waitTime)
        {
            open = true;
            timer = 0.0f;
            //spRend.enabled = false; //for flicking platform
            //gameObject.GetComponent<BoxCollider2D>().enabled = false; //x no need to set to false. Used dynamic collider setCollider in animator

            //animationFinished = false;
        }

        if (open && timer >= waitTime)
        {
            open = false;
            timer = 0.0f;
            //spRend.enabled = true;

            //animationFinished = false;
        }

        //if (!animationFinished)
        //{
        //    animationTimer += Time.deltaTime;
        //    if (animationTimer > animationTime)
        //    {
        //        animationFinished = true;
        //        animationTimer = 0.0f;
        //    }

        //}



        //if(animationFinished && open == false)
        //{
        //    gameObject.GetComponent<BoxCollider2D>().enabled = true;
        //    stopTimer = 0.0F;
        //    animationFinished = true;
        //}

        animatorSet();
    }

    void setPlatformCollider()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    void removePlatformCollider() 
    { 
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
