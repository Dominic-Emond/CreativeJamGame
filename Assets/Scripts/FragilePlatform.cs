using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragilePlatform : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private bool triggered;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void animatorSet()
    {
        animator.SetBool("triggered", triggered);
    }

    // Update is called once per frame
    void Update()
    {
        triggered = transform.parent.Find("TriggerArea").GetComponent<FragilePlatformTrigger>().triggered;
        animatorSet();
    }
}
