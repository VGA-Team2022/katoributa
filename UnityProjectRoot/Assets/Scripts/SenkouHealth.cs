using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;

/// <summary>
/// ��������i�̗́j�̋����𐧌䂷��R���|�[�l���g
/// </summary>
public class SenkouHealth : MonoBehaviour
{
    [Header("��������̎c�莞��")]
    [SerializeField, Tooltip("��������̎c�莞��")] FloatReactiveProperty _senkouTime = new FloatReactiveProperty(60f);
    [SerializeField, Tooltip("�����������[�h���̐����̌��鎞��")] float _onPowerUpSpeed = 2f;
    [Tooltip("���݂̌���X�s�[�h")] float _currentSpeed = 1f;
    [Tooltip("�|�[�Y���Ă��邩�ǂ���")] bool _isPaused;

    [Header("���G�t���O")]
    [SerializeField] bool _godMode;

    public IReactiveProperty<float> Health => _senkouTime;

    private void Update()
    {
        ReduceHealth();
    }

    /// <summary>
    /// ���X�ɑ̗͂������Ă�������
    /// </summary>
    private void ReduceHealth()
    {
        if (_godMode) return;

        if (_isPaused) return;

        _senkouTime.Value -= Time.deltaTime * _currentSpeed;

        _senkouTime.Value = Mathf.Max(0, _senkouTime.Value);

        if(_senkouTime.Value <= 0)
        {
            GameManager.Instance.OnGameEnd();
        }
    }

    private void OnPause()
    {
        _isPaused = true;
    }

    private void OnResume()
    {
        _isPaused = false;
    }

    private void EnabledPowerUp()
    {
        _currentSpeed = _onPowerUpSpeed;
    }

    private void DisabledPowerUp()
    {
        _currentSpeed = 1f;
    }

    private void OnEnable()
    {
        GameManager.Instance.OnPowerUpEvent += EnabledPowerUp;
        GameManager.Instance.OnPowerDownEvent += DisabledPowerUp;
        GameManager.Instance.OnPause += OnPause;
        GameManager.Instance.OnResume += OnResume;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPowerUpEvent -= EnabledPowerUp;
        GameManager.Instance.OnPowerDownEvent -= DisabledPowerUp;
        GameManager.Instance.OnPause -= OnPause;
        GameManager.Instance.OnResume -= OnResume;
    }

    /// <summary>
    /// �A�C�e�����擾�������ɌĂ΂��֐�
    /// </summary>
    /// <param name="value">�񕜃A�C�e���̉񕜒l</param>
    public void GetHeal(float value)
    {
        _senkouTime.Value += value;
        Debug.Log(value + "�񕜂���");
    }
}
