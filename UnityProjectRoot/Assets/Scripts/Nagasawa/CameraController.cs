using UnityEngine;
using Cinemachine;
using System;

/// <summary>
/// カメラを制御しているコンポーネント
/// </summary>
public class CameraController : MonoBehaviour
{
    [Tooltip("カメラのパターン"), SerializeField]
    CameraPattern _cameraPattern;

    [Tooltip("感度設定"), SerializeField] 
    float _xSensityvity,_ySensityvity = 3f;

    [Tooltip("Y軸の上限下限 パターン1"), SerializeField]
    float _minY = -2, _maxY = 7;

    [Tooltip("Y軸の上限下限 パターン2"), SerializeField]
    float _minYb = -45.0f, _maxYb = 45.0f;

    [Tooltip("カメラの追従度"), SerializeField]
    float _cameraDamping = 0.5f;

    [Tooltip("Playerのカメラ"), SerializeField] 
    CinemachineVirtualCamera _vcam;

    bool _cursorLock = true;
    Cinemachine3rdPersonFollow _tpf;
    CinemachineComposer _composer;
   
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

    void CameraState()
    {
        _composer.m_HorizontalDamping = _cameraDamping;
        _composer.m_VerticalDamping = _cameraDamping;
    }

    /// <summary>
    /// 最初のセットアップ
    /// </summary>
    void SetUp()
    {
        _tpf = _vcam.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        _composer = _vcam.GetCinemachineComponent<CinemachineComposer>();
    }

    /// <summary>
    /// カメラを動かしているところ
    /// </summary>
    void MoveCameraMethod()
    {
        switch (_cameraPattern)
        {
            case CameraPattern.Pattern1:
                transform.localRotation *= Quaternion.Euler(0, InputUtility.GetDirectionCameraMove.x * _xSensityvity / 10, 0);

                _tpf.VerticalArmLength -= InputUtility.GetDirectionCameraMove.y * _ySensityvity / 100;
                _tpf.VerticalArmLength = Mathf.Clamp(_tpf.VerticalArmLength, _minY, _maxY);
                break;
            case CameraPattern.Pattern2:
                transform.localRotation *= Quaternion.Euler(0, InputUtility.GetDirectionCameraMove.x * _xSensityvity / 10, 0);

                transform.localRotation *= Quaternion.Euler(-InputUtility.GetDirectionCameraMove.y * _ySensityvity / 10, 0, 0);
                transform.localRotation = ClampRotation(transform.localRotation);
                break;
            default:
                Debug.LogError("移動パターンを指定してください");
                break;
        }
    }

    /// <summary>
    /// カーソルをロックする関数　絶対ここに書かないほうがいい気がする
    /// </summary>
    public void UpdateCursorLock()
    {
        if (_cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (!_cursorLock)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    /// <summary>
    /// 角度制限関数の作成
    /// </summary>
    /// <param name="q"></param>
    /// <returns></returns>
    public Quaternion ClampRotation(Quaternion q)
    {
        //q = x,y,z,w (x,y,zはベクトル（量と向き）：wはスカラー（座標とは無関係の量）)
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1f;
        float angleX = Mathf.Atan(q.x) * Mathf.Rad2Deg * 2f;
        angleX = Mathf.Clamp(angleX, _minYb, _maxYb);
        q.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);
        return q;
    }

    /// <summary>
    /// プランナーさんに決めていただくパターン
    /// </summary>
    enum CameraPattern
    {
        Pattern1,
        Pattern2,
    }
}
