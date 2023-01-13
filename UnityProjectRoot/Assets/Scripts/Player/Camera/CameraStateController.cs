using UnityEngine;
using Cinemachine;

public class CameraStateController : MonoBehaviour
{
    CinemachineVirtualCamera _vcam;

    private void OnEnable()
    {
        GameManager.Instance.OnGameOverEvent += Pause;
        GameManager.Instance.OnPause += Pause;
        GameManager.Instance.OnResume += Resume;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameOverEvent -= Pause;
        GameManager.Instance.OnPause -= Pause;
        GameManager.Instance.OnResume -= Resume;
    }

    private void Start()
    {
        _vcam = GetComponent<CinemachineVirtualCamera>();
        //Pause();
    }

    void Pause()
    {
        _vcam.enabled = false;
    }

    void Resume()
    {
        _vcam.enabled = true;
    }
}
