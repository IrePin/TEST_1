using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] GameObject loaderCanvas;
    [SerializeField] GameObject menuCanvas;
    [SerializeField] private Slider progressBar;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }


    public async void LoadScene(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        loaderCanvas.SetActive(true);

        do
        {
            progressBar.value = scene.progress;
            await Task.Delay(100);
        } while (scene.progress <0.9f);

        await Task.Delay(100);
        scene.allowSceneActivation = true;
        loaderCanvas.SetActive(false);
        menuCanvas.SetActive(false);
    }
}