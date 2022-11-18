using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
        Debug.Log($"ƒV[ƒ“‚ªØ‚è‘Ö‚í‚è‚Ü‚µ‚½ : Scene = {sceneName}");
    }
}
