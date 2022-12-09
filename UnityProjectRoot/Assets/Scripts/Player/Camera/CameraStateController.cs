using UnityEngine;
using Cinemachine;

public class CameraStateController : MonoBehaviour
{
    CinemachineVirtualCamera _vcam;

    private void OnEnable()
    {
        GameManager.Instance.OnGameOverEvent += OnDead;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameOverEvent -= OnDead;
    }

    private void Start()
    {
        _vcam = GetComponent<CinemachineVirtualCamera>();
    }

    void OnDead()
    {
        _vcam.enabled = false;
    }
}
