using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loadingscene : MonoSingleTone<Loadingscene>
{
    public float time;
    public AsyncOperation load;
    

    private void Awake()
    {
        StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene()
    {
        yield return null;
        load = SceneManager.LoadSceneAsync(SceneSave.sceneName);
        load.allowSceneActivation = false;
        while (!load.isDone)
        {
            time += Time.deltaTime;
            yield return null;
            if (load.progress >= 0.9f && time >= 6)
            {
                load.allowSceneActivation = true;
            }
        }
    }
}
