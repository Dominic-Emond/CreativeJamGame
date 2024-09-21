using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float movedDistance = 0;
    [SerializeField] private float moveSpeed = 2;
    [SerializeField] private float maximumMovingDistance = 5;
    [SerializeField] private bool moveHorizontally;
    [SerializeField] private bool moveVertically;
    [SerializeField] private Vector3 direction; // 1 as positive direction
    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector3(moveHorizontally ? 1.0f : 0.0f, moveVertically ? 1.0f : 0.0f, 0);//toggle the variables before starting the game; //System.Convert.ToSingle(moveHorizontally) for runtime

    }

    // Update is called once per frame
    void Update()
    {
        movedDistance += Time.deltaTime * moveSpeed;
        Vector3 temp = new Vector3(transform.position.x + Time.deltaTime * moveSpeed * direction.x, transform.position.y + Time.deltaTime * moveSpeed * direction.y, 0); // also move down with the level
        if(movedDistance > maximumMovingDistance)
        {
            direction = -direction;
            movedDistance = 0;
        }
        transform.position = temp;
    }
}
