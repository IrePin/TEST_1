using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadButton : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        LevelManager.instance.LoadScene(sceneName);
    }

}
