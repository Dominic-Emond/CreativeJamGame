using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMove : MonoBehaviour
{
   
    [SerializeField] private Vector3 location;
    [SerializeField] private float moveSpeed = 1;
    bool finished = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    void Moving()
    {
        if (!finished)
        {
            Vector3 temp = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * moveSpeed, 0);
            transform.position = temp;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
    }

}
