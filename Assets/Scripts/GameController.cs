using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(GameUI))]
public class GameController : MonoBehaviour
{
    

    public static GameController Instance { get; private set; }
    public GameUI GameUI { get; private set; }
    public SoundManager SoundManager { get; private set; }
    public SwordScript SwordScript { get; private set; }
   
    [SerializeField] private int SwordCount;

    [SerializeField] private GameObject swordObject;
    [SerializeField] private GameObject barrel;
    [SerializeField] private GameObject apple;
    [SerializeField] private Transform barrelSpawn;
    [SerializeField] private Transform hitTransformPosition;
    [SerializeField] private Animator barrelAnimator;
    
    [SerializeField] private Transform gameOverVfxPrefab;
    [SerializeField] private Transform appleHitVfxPrefab;
    [SerializeField] private Transform HitVfxPrefab;


    private Vector3 swordSpawnPosition;
    private Vector3 appleSpawnPosition;

    private SoundManager soundManager;

    

    
   
    private void Awake()
    {
        
        hitTransformPosition.position = hitTransformPosition.gameObject.transform.position;
        hitTransformPosition.rotation = hitTransformPosition.gameObject.transform.rotation;
        swordSpawnPosition = new Vector3(5, -3, -10);
        appleSpawnPosition = new Vector3(5, 11, -10);
        Instance = this;
        GameUI = GetComponent<GameUI>();
        soundManager = GetComponentInChildren<SoundManager>();
    }

    private void Start()
    {
        GameUI.SetInitialDisplayedSwordCount(SwordCount);
        barrel = Instantiate(barrel, barrelSpawn.transform.position, Quaternion.Euler(90, 0, 0));
                apple = Instantiate(apple, appleSpawnPosition, Quaternion.identity);
        apple.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        apple.transform.parent = barrel.transform;
        transform.tag = "Apple";
       // barrel.transform.SetParent = barrelSpawn.transform;
        SpawnSword();
    }

    
    private IEnumerator TimeDelay()
        {
        yield return new WaitForSeconds(15);
        }


    
    public void OnSuccessfulSwordHit()
    {
        if (SwordCount > 0)
        {
            Instantiate(HitVfxPrefab, hitTransformPosition.position, Quaternion.identity);
            soundManager.PlayHit();
            Debug.Log("hit");
            SpawnSword();

        }
        else if (SwordCount == 0)
        {
            TimeDelay();
            StartGameOverSequence(true);
        }

    }
    public void OnSuccessfulAppledHit()
    {
        Instantiate(appleHitVfxPrefab, hitTransformPosition.position, Quaternion.identity);
        Destroy(GameObject.FindWithTag("Apple"));
        TimeDelay();
        GameUI.ShowRestartButton();

    }


    private void SpawnSword()
    {
        SwordCount--;
        Instantiate(swordObject, swordSpawnPosition, Quaternion.identity);
    }


    public void StartGameOverSequence(bool win)
    {
        StartCoroutine("GameOverSequenceCoroutine", win);
    }

    private IEnumerator GameOverSequenceCoroutine(bool win)
    {
        if (win)
        {
            yield return null;
            TimeDelay();
            GameUI.ShowRestartButton();
        }
        else
        {
            Instantiate(gameOverVfxPrefab, new Vector3(5, 8.8f, -10), Quaternion.identity); 
            Destroy(barrel);
            TimeDelay();
            GameUI.ShowRestartButton();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }


}