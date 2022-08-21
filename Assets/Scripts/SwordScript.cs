using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public SoundManager SoundManager { get; private set; }

    [SerializeField] private float speed;
    [SerializeField] GameObject sword;

    private AudioSource swoop;
       
    public bool isActive = true;

    private Rigidbody rb;
    private BoxCollider swordCollider;
            

       private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        swordCollider = GetComponent<BoxCollider>();
        swoop = GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            rb.AddForce(Vector3.up * 23, ForceMode.Impulse);
            swoop.Play();

        }

    }

    public void OnTriggerEnter(Collider other)
    {
        
        if (!isActive)
        {
            transform.tag = "SwordLog";
            swordCollider.isTrigger = true;
            return;
        }

            

        Debug.Log(isActive);

        if (other.tag == "Apple")
        {
           GameController.Instance.OnSuccessfulAppledHit();

        }


        if (other.tag == "Barrel")
        {
            GameController.Instance.GameUI.DecrementDisplayedSwordCount();

            transform.SetParent(other.transform);
            rb.isKinematic = true;
            rb.useGravity = false;
            isActive = false;

            GameController.Instance.OnSuccessfulSwordHit();
            Debug.Log("InBarrel");
        }
        
        else if (other.tag == "SwordLog")
        {
            Destroy(gameObject);
           
            GameController.Instance.StartGameOverSequence(false);
        }

    }
}

