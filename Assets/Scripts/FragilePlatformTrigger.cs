using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FragilePlatformTrigger : MonoBehaviour
{
    [SerializeField] private float clapsingTime;
    [SerializeField] private bool triggered = false;
    private float timer;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            triggered = true;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered)
        {
            timer += Time.deltaTime;
            //playAnimationAfterTriggered
        }
        if(timer > clapsingTime)
        {
            //Destroy rigid body of the platform
            Destroy(gameObject); //test 
        }
    }
}
