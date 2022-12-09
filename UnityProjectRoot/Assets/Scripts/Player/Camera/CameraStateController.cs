using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraStateController : MonoBehaviour
{
    CinemachinePOV _pov;

    private void OnEnable()
    {
        GameManager.Instance.OnGameOverEvent += OnDead;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameOverEvent -= OnDead;
    }

    void Start()
    {
        _pov = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachinePOV>();
    }

    void OnDead()
    {
        _pov.m_VerticalAxis.m_MinValue = 0;
        _pov.m_VerticalAxis.m_MaxValue = 0;
        _pov.m_HorizontalAxis.m_MinValue = 0;
        _pov.m_HorizontalAxis.m_MaxValue = 0;
    }
}
