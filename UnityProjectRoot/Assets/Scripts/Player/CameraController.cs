using UnityEngine;
using Cinemachine;
using System;

/// <summary>
/// �J�����𐧌䂵�Ă���R���|�[�l���g
/// </summary>
public class CameraController : MonoBehaviour
{
    [Tooltip("���x�ݒ�"), SerializeField] 
    float _xSensityvity,_ySensityvity = 3f;

    [Tooltip("�J�����̒Ǐ]�x"), SerializeField]
    float _cameraDamping = 0.5f;

    [Tooltip("Player�̃J�����̃I�u�W�F�N�g"), SerializeField]
    GameObject _cam;

    [Tooltip("Player�̃J�����̃V�l�}�V��"), SerializeField] 
    CinemachineVirtualCamera _vcam;

    bool _cursorLock = true;
   
    void Start()
    {
        SetUp();
    }

    void Update()
    {
        if (GameManager.Instance.GameState == GameState.GameFinish) return;
        CameraState();
        MoveCameraMethod();
        UpdateCursorLock();
    }

    /// <summary>
    /// �ŏ��̃Z�b�g�A�b�v
    /// </summary>
    void SetUp()
    {

    }

    /// <summary>
    /// �J�����̏�Ԃ�ύX����
    /// </summary>
    void CameraState()
    {

    }

    /// <summary>
    /// �J�����𓮂����Ă���Ƃ���
    /// </summary>
    void MoveCameraMethod()
    {
        transform.localRotation = _cam.transform.localRotation;
    }

    /// <summary>
    /// �J�[�\�������b�N����֐��@��΂����ɏ����Ȃ��ق��������C������
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
