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
    [SerializeField] private GameObject BarreSpawn;
    [SerializeField] private Animator barrelAnimator;
    [SerializeField] private Transform gameOverVfxPrefab;
    [SerializeField] private Transform appleHitVfxPrefab;
    [SerializeField] private Transform HitVfxPrefab;
        
    private Vector3 hitTransformPosition;
    private Vector3 swordSpawnPosition;
    private Vector3 appleSpawnPosition;

    private SoundManager soundManager;

    

    
   
    private void Awake()
    {
        hitTransformPosition = new Vector3(5, 7.6f, -10);
        swordSpawnPosition = new Vector3(5, -3, -10);
        appleSpawnPosition = new Vector3(5, 11, -10);
        Instance = this;
        GameUI = GetComponent<GameUI>();
        soundManager = GetComponentInChildren<SoundManager>();
    }

    private void Start()
    {
        GameUI.SetInitialDisplayedSwordCount(SwordCount);
        apple = Instantiate(apple, appleSpawnPosition, Quaternion.identity);
        apple.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        apple.transform.parent = barrel.transform;
        transform.tag = "Apple";
        SpawnSword();
    }

    
    private IEnumerator TimeDelay()
        {
            yield return new WaitForSeconds(3);
        }


    
    public void OnSuccessfulSwordHit()
    {
        if (SwordCount > 1)
        {
            Instantiate(HitVfxPrefab, hitTransformPosition, Quaternion.identity);
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
        Instantiate(appleHitVfxPrefab, hitTransformPosition, Quaternion.identity);
        Destroy(GameObject.FindWithTag("Apple"));
             
        TimeDelay();

        DontDestroyOnLoad(gameObject);

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
            yield return new WaitForSecondsRealtime(3f);
            GameUI.ShowRestartButton();
        }
        else
        {
            Instantiate(gameOverVfxPrefab, new Vector3(5, 8.8f, -10), Quaternion.identity); 
            Destroy(barrel);
            GameUI.ShowRestartButton();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }


}