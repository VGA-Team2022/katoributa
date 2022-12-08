using UnityEngine;
using Cinemachine;
using System;

/// <summary>
/// カメラを制御しているコンポーネント
/// </summary>
public class CameraController : MonoBehaviour
{
    [Tooltip("感度設定"), SerializeField] 
    float _xSensityvity,_ySensityvity = 3f;

    [Tooltip("カメラの追従度"), SerializeField]
    float _cameraDamping = 0.5f;

    [Tooltip("Playerのカメラのオブジェクト"), SerializeField]
    GameObject _cam;

    [Tooltip("Playerのカメラのシネマシン"), SerializeField] 
    CinemachineVirtualCamera _vcam;

    bool _cursorLock = true;
   
    void Start()
    {
        SetUp();
    }

    void Update()
    {
        CameraState();
        MoveCameraMethod();
        UpdateCursorLock();
    }

    /// <summary>
    /// 最初のセットアップ
    /// </summary>
    void SetUp()
    {

    }

    /// <summary>
    /// カメラの状態を変更する
    /// </summary>
    void CameraState()
    {

    }

    /// <summary>
    /// カメラを動かしているところ
    /// </summary>
    void MoveCameraMethod()
    {
        transform.localRotation = _cam.transform.localRotation;
    }

    /// <summary>
    /// カーソルをロックする関数　絶対ここに書かないほうがいい気がする
    /// </summary>
    public void UpdateCursorLock()
    {
        //if (_cursorLock)
        //{
        //    Cursor.lockState = CursorLockMode.Locked;
        //}
        //else if (!_cursorLock)
        //{
        //    Cursor.lockState = CursorLockMode.None;
        //}
    }
}
