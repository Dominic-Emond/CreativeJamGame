using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class FragilePlatformTrigger : MonoBehaviour
{
    [SerializeField] private float clapsingTime;
    [SerializeField] public bool triggered = false;
    [SerializeField] private ParticleSystem particles;

    private float timer;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            triggered = true;
        }
        particles.Play();
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
            Destroy(gameObject.transform.parent.gameObject); //test     
        }
    }
}
