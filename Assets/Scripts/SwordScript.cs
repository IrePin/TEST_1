using UnityEngine;

public class SwordScript : MonoBehaviour
{
    
    [SerializeField] private float speed;
    [SerializeField] GameObject sword;

    public bool isActive = true;

    private Rigidbody rb;
    private BoxCollider swordCollider;
    private AudioSource swoopSource;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        swordCollider = GetComponent<BoxCollider>();
        swoopSource = GetComponentInChildren<AudioSource>();
    }


    public void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            rb.AddForce(Vector3.up * 23, ForceMode.Impulse);
            Debug.Log("input");
            swoopSource.Play();
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
           GameController.Instance.GameUI.upDisplayedAppleCount();
           Destroy(other.gameObject);
           GameController.Instance.OnSuccessfulAppledHit();
        }


        if (other.tag == "Barrel")
        {
            GameController.Instance.GameUI.DecrementDisplayedSwordCount();

            transform.SetParent(other.transform);
            Destroy(GetComponent<SwordScript>());
            Destroy(GetComponentInChildren<AudioSource>());
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

