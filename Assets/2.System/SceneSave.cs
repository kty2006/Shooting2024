using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSave : MonoSingleTone<SceneSave>
{
    public static int sceneName;
    public void Scene(int SceneName)
    {
        SceneManager.LoadScene(2);
        sceneName = SceneName;
    }
    public void Start()
    {
        DontDestroyOnLoad(this);
    }
}
