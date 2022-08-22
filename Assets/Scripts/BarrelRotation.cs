using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelRotation : MonoBehaviour
{
       
    [SerializeField] private float speed;
    [SerializeField] Animator animator;

    public AudioSource audioSrs;

    private float rotation;
    private float increase;
    
    public void Start()
    {
        increase = 1.2f;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0 );
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sword")
        {
            animator.Play("BarrelHitAnim");
            speed = speed * increase;
            Debug.Log("increase");

        }
    }

    
    }

