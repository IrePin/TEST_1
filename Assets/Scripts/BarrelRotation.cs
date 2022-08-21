using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelRotation : MonoBehaviour
{
       
    [SerializeField] private float speed = 10;
    
    private float increase;
    private float decrease;

    public void Start()
    {
        increase = 1.2f;
        decrease = speed / increase;
    }

    void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0 );
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sword")
        {
            speed *= increase;
            Debug.Log("increase");
        }
    }
   
}