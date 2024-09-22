using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public float currentHealth { get; private set; }
    [SerializeField] private float maxHealth;
    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        if (currentHealth > 0)
        {

        }
        else
        {

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
