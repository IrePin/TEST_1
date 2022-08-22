using System;
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
      
    [SerializeField] private int SwordCount;

    [SerializeField] private GameObject swordObject;
    [SerializeField] private GameObject barrel;
    [SerializeField] private GameObject world;
    [SerializeField] private GameObject[] apple;
    [SerializeField] private Transform barrelSpawn;
    [SerializeField] private Transform hitTransformPosition;
    [SerializeField] private Transform gameOverVfxPrefab;
    [SerializeField] private Transform appleHitVfxPrefab;
    [SerializeField] private Transform HitVfxPrefab;


    private Vector3 swordSpawnPosition;
    private Vector3 appleSpawnPosition;

    private int direction;
    
  
    private void Awake()
    {
        Instance = this;
        hitTransformPosition.position = hitTransformPosition.gameObject.transform.position;
        hitTransformPosition.rotation = hitTransformPosition.gameObject.transform.rotation;
        swordSpawnPosition = new Vector3(5, -3, -10);
        GameUI = GetComponent<GameUI>();
        SpawnSword();
    }

    private void Start()
    {
        GameUI.SetInitialDisplayedSwordCount(SwordCount);
        barrel = Instantiate(barrel, barrelSpawn.transform.position, Quaternion.Euler(90, 0, 0));
        world = Instantiate(world, new Vector3(7.41f, 1.2f, -2.9f), Quaternion.identity);
        spawnApple(3, barrelSpawn.transform.position, 1.52f);
                     
    }


    



    public void OnSuccessfulSwordHit()
    {
        if (SwordCount > 0)
        {
            Instantiate(HitVfxPrefab, hitTransformPosition.position, Quaternion.identity);
            SpawnSword();

        }
        else if (SwordCount == 7)
        {
            SpawnSword( );
            TimeDelay();
            StartGameOverSequence(true);
        }

    }
    
    


    public void OnSuccessfulAppledHit()
    {

        Instantiate(appleHitVfxPrefab, hitTransformPosition.position, Quaternion.identity);
        StartCoroutine(TimeDelay());
         

    }


    private void SpawnSword()
    {
        SwordCount--;
        Instantiate(swordObject, swordSpawnPosition, Quaternion.identity);
    }

    private void spawnApple(int apples, Vector3 point, float radius)
    {
        for(int i = 0; i < apples; i++){

                       var radians = 2 * MathF.PI / apples * i;

            
            var vertical = MathF.Sin(radians);
            var horizontal = MathF.Cos(radians);

            var spawnDir = new Vector3(vertical + 0.08f, horizontal , 0);

            var spawnPos = point + spawnDir * radius; 

            Instantiate(apple[i], spawnPos, Quaternion.identity, barrel.transform);
            
            apple[i].transform.LookAt(barrelSpawn);
        }
}

    public void StartGameOverSequence(bool win)
    {
        StartCoroutine("GameOverSequenceCoroutine", win);
    }

    private IEnumerator GameOverSequenceCoroutine(bool win)
    {
        if (win)
        {
            yield return new WaitForSecondsRealtime(5);
            GameUI.ShowRestartButton();
        }
        else
        {
            Instantiate(gameOverVfxPrefab, new Vector3(5, 8.8f, -10), Quaternion.identity); 
            Destroy(barrel);
            
            GameUI.ShowRestartButton();
            StopCoroutine(TimeDelay());
        }
        
    }

    

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void GameOver()
    {
        
    }
    IEnumerator TimeDelay()
            {
        
             yield return new WaitForSecondsRealtime(5);
                
        }
        
}