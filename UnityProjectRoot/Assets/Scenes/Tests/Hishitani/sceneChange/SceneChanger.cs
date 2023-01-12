using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
        Debug.Log($"�V�[�����؂�ւ��܂��� : Scene = {sceneName}");
    }

    public void ResetScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
        Debug.Log($"�V�[�����ēx�ǂݍ��܂�܂��� : Scene = {sceneName}");
    }
}
