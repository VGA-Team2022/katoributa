using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
        Debug.Log($"シーンが切り替わりました : Scene = {sceneName}");
    }
}
