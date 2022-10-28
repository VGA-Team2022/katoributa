using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChange : MonoBehaviour
{
    [SerializeField]Animator _panelAnim;
    string _name;
    public void change(string _sceneName)
    {
        //Debug.Log("ositeru");
        _name = _sceneName;
        _panelAnim.Play("panel");

    }
    void Change()
    {
        SceneManager.LoadScene(_name);
    }
}
